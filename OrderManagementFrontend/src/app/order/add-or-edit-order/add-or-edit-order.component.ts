import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
import { Person } from 'src/app/person/person';
import { ProductItem } from 'src/app/product-item/product-item';
import { OrderService } from 'src/app/services/order.service';
import { Order } from '../order';

@Component({
  selector: 'app-add-or-edit-order',
  templateUrl: './add-or-edit-order.component.html',
  styleUrls: ['./add-or-edit-order.component.scss']
})
export class AddOrEditOrderComponent implements OnInit {

  isEdit: boolean = false;
  orderform: FormGroup = new FormGroup({
    orderNo: new FormControl('', Validators.required),
    productName: new FormControl('', Validators.required),
    orderDate: new FormControl(new Date(), Validators.required),
    total: new FormControl('', Validators.required),
    price: new FormControl('', Validators.required),
    totalPrice: new FormControl({value:0, disabled: true}),
    createdBy: new FormControl('', Validators.required)
  });
  public onClose!: Subject<boolean>;
  submitted: boolean = false;
  loader: boolean = false;
  order!: Order;
  modalDataPass: any;
  productItemlist: ProductItem []= [];
  personlist: Person []= [];
  maxDate: Date = new Date()
  constructor(private bsModalRef: BsModalRef, private modalService: BsModalService,
    private formBuilder: FormBuilder, private orderService: OrderService,
    public options: ModalOptions) {
     
     }

 ngOnInit(): void {
   this.onClose = new Subject();
   this.modalDataPass = this.options.initialState;
   if (this.modalDataPass && this.modalDataPass != null) {
    this.productItemlist = this.modalDataPass.productItemList;
    this.personlist = this.modalDataPass.personList;
     if (this.modalDataPass.isEdit) {
       this.isEdit = true;
       this.order = this.modalDataPass.order;
       this.updateEditInformation(this.order);
     }
   }

 }

 onSubmit(value: any): void {
   this.submitted = true;
   if (this.orderform.invalid) {
     return;
   }
   if(!this.isEdit){
     this.loader = true;
     value = this.orderform.getRawValue();
     this.orderService.addOrder(value).subscribe((res: any) => {
         // this.toastr.success('Added Successfully');
         this.cancel(true);
       },
       (error: Error) => {
         console.log(error);
         // this.toastr.error('Couldnot add callflow');
       }, () => { this.loader = false; });
   }else{
    this.loader = true;
    value = this.orderform.getRawValue();
    this.orderService.updateOrder(value.id,value).subscribe((res: any) => {
        // this.toastr.success('Added Successfully');
        this.cancel(true);
      },
      (error: Error) => {
        console.log(error);
        // this.toastr.error('Couldnot add callflow');
      }, () => { this.loader = false; });
   }
 }

 cancel(state: boolean): void {
   this.onClose.next(state);
   this.bsModalRef.hide();
 }

 updateEditInformation(order: Order): void {
   this.orderform = new FormGroup({
     id: new FormControl(order.id, Validators.required),
     orderNo: new FormControl(order.orderNo, Validators.required),
     orderDate: new FormControl(new Date(order.orderDate), Validators.required),
     productName: new FormControl(order.productName, Validators.required),
     total: new FormControl(order.total, Validators.required),
     price: new FormControl(order.price, Validators.required),
     totalPrice: new FormControl({value: order.totalPrice, disabled: true}),
     createdBy: new FormControl(order.createdBy, Validators.required)
   });
 }

 onChangeTotal(value: number){
  if(this.orderform.controls['price'].value){
    let totalprice = value * Number(this.orderform.controls['price'].value);
    this.orderform.get('totalPrice')?.patchValue(totalprice);
  }
 }

 onChangePrice(value: number){
  if(this.orderform.controls['total'].value){
    let totalprice = value * Number(this.orderform.controls['total'].value);
    this.orderform.get('totalPrice')?.patchValue(totalprice);
  }
 }


}
