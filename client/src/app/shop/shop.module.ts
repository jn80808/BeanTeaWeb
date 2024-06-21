import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { UniquePipe } from './unique.pipe';




@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent,
    UniquePipe
  ],
  imports: [
    CommonModule
  ],
  exports:[
    ShopComponent
  ]
})
export class ShopModule { }
