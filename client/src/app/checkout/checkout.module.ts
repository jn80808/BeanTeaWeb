import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChecoutComponent } from './checout.component';
import { Route, RouterModule, Routes } from '@angular/router';
import { CheckoutRoutingModule } from './checkout-routing.module';



@NgModule({
  declarations: [
    ChecoutComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule
  ]
})
export class CheckoutModule { }
