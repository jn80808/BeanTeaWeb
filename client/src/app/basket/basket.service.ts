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

  addItemToBasket(item: Product, quantity = 1) {
    const itemToAdd = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
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




}
