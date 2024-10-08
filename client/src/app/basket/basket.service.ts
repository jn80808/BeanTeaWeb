import { Injectable } from '@angular/core';
import { BehaviorSubject, retry } from 'rxjs';
import { environment } from 'src/environment/environment';
import { Basket, BasketItem, BasketTotals } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/products';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null); // observable
  basketSource$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<BasketTotals | null >(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }


  getBasket(id: string) {
    return this.http.get<Basket>(`${this.baseUrl}basket?id=${id}`).subscribe({ 
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });
  }

  setBasket(basket: Basket) {
    return this.http.post<Basket>(`${this.baseUrl}basket`, basket).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  // Add Quantity basket Item
  addItemToBasket(item: Product | BasketItem, quantity = 1) {
    //if ('productBrand' in item) this.mapProductItemToBasketItem(item);
    if (this.isProduct(item)) item = this.mapProductItemToBasketItem(item);
    console.log(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);
    this.setBasket(basket);
  }

  // Minus Quantity basket Item
  removeItemFromBasket(id:number , quantity=1){
    const basket = this.getCurrentBasketValue();
    if (!basket) return ;
    const item = basket.items.find(x => x.id === id);
    if (item) {
      item.quantity -= quantity;
      if (item.quantity === 0){
        basket.items =  basket.items.filter(x => x.id !==id);
      }
      if (basket.items.length > 0) this.setBasket(basket);
      else this.deleteBasket(basket);
    }
  }
  deleteBasket(basket: Basket) {
   return this.http.delete(this.baseUrl + 'basket?id' + basket.id).subscribe({
    next: () => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }
  
  })
  }

  private addOrUpdateItem(items: BasketItem[], itemToAdd: BasketItem, quantity: number): BasketItem[] {
    const existingItem = items.find(x => x.id === itemToAdd.id);
    if (existingItem) {
      existingItem.quantity += quantity;
    } else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  // mapping productitem to basket for cart
  private mapProductItemToBasketItem(item: Product): BasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      imageUrl: item.imageUrl,
      brand: item.productBrand,
      type: item.productType
    };
  }

  private calculateTotals(){
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const shipping = 0;
    const subtotal = basket.items.reduce((a,b) => (b.price * b.quantity) + a , 0); 
    const total = subtotal + shipping;
    this.basketTotalSource.next({shipping, total, subtotal});
  }

  private isProduct(item: Product | BasketItem) : item is Product {
    return (item as Product).productBrand !== undefined;
  }

}
