import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
import { Person } from '../person/person';
import { ProductItem } from '../product-item/product-item';
import { OrderService } from '../services/order.service';
import { PersonService } from '../services/person.service';
import { ProductItemService } from '../services/product-item.service';
import { DeleteComponent } from '../shared/delete/delete.component';
import { AddOrEditOrderComponent } from './add-or-edit-order/add-or-edit-order.component';
import { Order } from './order';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  public onClose!: Subject<boolean>;
  public modalRef!: BsModalRef;
  orderList: Order []= [];
  productItemlist: ProductItem []= [];
  personlist: Person []= [];
  onOrderSearchform: FormGroup = new FormGroup({
    email: new FormControl('', Validators.required),
  });
  submitted: boolean = false;
  selectedEmail!: string;

  constructor(private modalService: BsModalService, private orderService: OrderService, 
    private personService: PersonService, private productItemService: ProductItemService) { }

  ngOnInit(): void {
    this.onClose = new Subject();
    this.getCallPersonList();
    this.getCallProductItemList();
  }

  onSubmit(value: any): void{
    this.submitted = true;
    if (this.onOrderSearchform.invalid) {
      return;
    }
    this.getCallOrderList(value.email);
  }

  onChangeEmail($event: any) {
    this.selectedEmail = $event.target.value.toString();
  }

  getCallPersonList(){
    this.personService.getPersonList().subscribe((res: any) => {
      this.personlist = res;
    },
    (error: Error) => {
      console.log(error);
    }, () => { 
      // this.loader = false; 
    });
  }

  getCallProductItemList(){
    this.productItemService.getProductItemList().subscribe((res: any) => {
      this.productItemlist = res;
    },
    (error: Error) => {
      console.log(error);
    }, () => { 
      // this.loader = false; 
    });
  }


  getCallOrderList(email: string){
    this.orderService.getOrderList(email).subscribe((res: any) => {
     // console.log(res);
      this.orderList = res;
    },
    (error: Error) => {
      console.log(error);
      // this.toastr.error('Couldnot add callflow');
    }, () => { 
      // this.loader = false; 
    });
  }

  onOpenModal(): void {
    const initialState = {
      isEdit: false,
      personList: this.personlist,
      productItemList: this.productItemlist
    };
    let modalConfig = { animated: true, keyboard: true, backdrop: true, ignoreBackdropClick: false };
    this.modalRef = this.modalService.show(AddOrEditOrderComponent, Object.assign({}, modalConfig, {class: 'modal-md', initialState}));
    this.modalRef.content.onClose.subscribe((result: boolean) => {
      if (result && this.selectedEmail!= null ) {
        this.getCallOrderList(this.selectedEmail);
      }
    });
  }


  onEditRow(order: Order): void {
    const initialState = {isEdit: true, order: order, personList: this.personlist,
      productItemList: this.productItemlist };
    let modalConfig = { animated: true, keyboard: true, backdrop: true, ignoreBackdropClick: false };
    this.modalRef = this.modalService.show(AddOrEditOrderComponent, Object.assign({}, modalConfig, {class: 'modal-md', initialState}));
    this.modalRef.content.onClose.subscribe((result: boolean) => {
      if (result) {
        this.getCallOrderList(this.selectedEmail);
      }
    });
  }

  onDeleteRow(order: Order, index: number): void {
    const initialState = {
      // backdrop: 'static',
      keyboard: false,
      backdrop: true,
      ignoreBackdropClick: true,
    };

    this.modalRef = this.modalService.show(DeleteComponent, initialState);
    this.modalRef.content.onClose.subscribe((result: any) => {
      if (result.Action === 'DELETE') {
        this.delete(order, index);
      } else {

      }
    });
  }

  delete(order: Order, index: number): void{
      //this.loader = true;
      this.orderService.deleteOrder(order.id).subscribe((res: any) => {
        // this.toastr.success('Added Successfully');
        this.orderList.splice(index, 1)
      },
      (error: Error) => {
        console.log(error);
        // this.toastr.error('Couldnot add callflow');
      }, () => { //this.loader = false; 
      });
  }

}
