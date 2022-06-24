import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductItemRoutingModule } from './product-item-routing.module';
import { ProductItemComponent } from './product-item.component';
import { SharedModule } from '../shared/shared.module';
import { AddOrEditProductItemComponent } from './add-or-edit-product-item/add-or-edit-product-item.component';


@NgModule({
  declarations: [
    ProductItemComponent,
    AddOrEditProductItemComponent
  ],
  imports: [
    CommonModule,
    ProductItemRoutingModule,
    SharedModule
  ]
})
export class ProductItemModule { }
