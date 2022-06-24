import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OrderModule } from './order/order.module';
import { PersonModule } from './person/person.module';
import { ProductItemModule } from './product-item/product-item.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    PersonModule,
    OrderModule,
    ProductItemModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
