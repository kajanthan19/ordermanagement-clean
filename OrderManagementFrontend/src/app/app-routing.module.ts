import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path:'',
    redirectTo: 'order',
    pathMatch: 'full'
  },
  {
    path: 'person',
    loadChildren: () => import('./person/person-routing.module').then(m => m.PersonRoutingModule)
  },
  {
    path: 'order',
    loadChildren: () => import('./order/order-routing.module').then(m => m.OrderRoutingModule)
  },
  {
    path: 'product-item',
    loadChildren: () => import('./product-item/product-item-routing.module').then(m => m.ProductItemRoutingModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
