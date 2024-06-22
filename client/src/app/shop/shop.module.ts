import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { UniquePipe } from './unique.pipe';
import { SharedModule } from '../shared/shared.module';




@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent,
    UniquePipe
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports:[
    ShopComponent
  ]
})
export class ShopModule { }
