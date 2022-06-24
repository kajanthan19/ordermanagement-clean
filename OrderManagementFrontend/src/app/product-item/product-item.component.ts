import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
import { ProductItemService } from '../services/product-item.service';
import { DeleteComponent } from '../shared/delete/delete.component';
import { AddOrEditProductItemComponent } from './add-or-edit-product-item/add-or-edit-product-item.component';
import { ProductItem } from './product-item';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {

  public onClose!: Subject<boolean>;
  public modalRef!: BsModalRef;
  productItemlist: ProductItem []= [];
  constructor(private modalService: BsModalService, private productItemService: ProductItemService) { }

  ngOnInit(): void {
    this.onClose = new Subject();
    this.getCallProductItemList();
  }

  getCallProductItemList(){
    this.productItemService.getProductItemList().subscribe((res: any) => {
      console.log(res);
      this.productItemlist = res;
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
      isEdit: false
    };
    let modalConfig = { animated: true, keyboard: true, backdrop: true, ignoreBackdropClick: false };
    this.modalRef = this.modalService.show(AddOrEditProductItemComponent, Object.assign({}, modalConfig, {class: 'modal-md', initialState}));
    this.modalRef.content.onClose.subscribe((result: boolean) => {
      if (result) {
        this.getCallProductItemList();
      }
    });
  }


  onEditRow(product: ProductItem): void {
    const initialState = {isEdit: true, product: product };
    let modalConfig = { animated: true, keyboard: true, backdrop: true, ignoreBackdropClick: false };
    this.modalRef = this.modalService.show(AddOrEditProductItemComponent, Object.assign({}, modalConfig, {class: 'modal-md', initialState}));
    this.modalRef.content.onClose.subscribe((result: boolean) => {
      if (result) {
        this.getCallProductItemList();
      }
    });
  }

  onDeleteRow(product: ProductItem, index: number): void {
    const initialState = {
      // backdrop: 'static',
      keyboard: false,
      backdrop: true,
      ignoreBackdropClick: true,
    };

    this.modalRef = this.modalService.show(DeleteComponent, initialState);
    this.modalRef.content.onClose.subscribe((result: any) => {
      if (result.Action === 'DELETE') {
        this.delete(product, index);
      } else {

      }
    });
  }

  delete(product: ProductItem, index: number): void{
    //this.loader = true;
    this.productItemService.deleteProductItem(product.id).subscribe((res: any) => {
        // this.toastr.success('Added Successfully');
        this.productItemlist.splice(index, 1)
      },
      (error: Error) => {
        console.log(error);
        // this.toastr.error('Couldnot add callflow');
      }, () => { //this.loader = false; 
      });
  }

}
