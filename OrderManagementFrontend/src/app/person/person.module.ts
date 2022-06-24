import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PersonRoutingModule } from './person-routing.module';
import { PersonComponent } from './person.component';
import { SharedModule } from '../shared/shared.module';
import { AddOrEditPersonComponent } from './add-or-edit-person/add-or-edit-person.component';


@NgModule({
  declarations: [
    PersonComponent,
    AddOrEditPersonComponent
  ],
  imports: [
    CommonModule,
    PersonRoutingModule,
    SharedModule
  ]
})
export class PersonModule { }
