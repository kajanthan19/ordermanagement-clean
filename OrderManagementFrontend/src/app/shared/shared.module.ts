import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PersonService } from '../services/person.service';
import { DeleteComponent } from './delete/delete.component';

@NgModule({
  declarations: [
    DeleteComponent
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
  ],
  providers:[PersonService],
  exports:[ 
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ModalModule,
    BsDatepickerModule,
  ],
  entryComponents:[DeleteComponent]
})
export class SharedModule { }
