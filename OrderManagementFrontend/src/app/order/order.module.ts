import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderRoutingModule } from './order-routing.module';
import { OrderComponent } from './order.component';
import { SharedModule } from '../shared/shared.module';
import { AddOrEditOrderComponent } from './add-or-edit-order/add-or-edit-order.component';


@NgModule({
  declarations: [
    OrderComponent,
    AddOrEditOrderComponent
  ],
  imports: [
    CommonModule,
    OrderRoutingModule,
    SharedModule
  ]
})
export class OrderModule { }
