import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
import { ProductItemService } from 'src/app/services/product-item.service';
import { ProductItem } from '../product-item';

@Component({
  selector: 'app-add-or-edit-product-item',
  templateUrl: './add-or-edit-product-item.component.html',
  styleUrls: ['./add-or-edit-product-item.component.scss']
})
export class AddOrEditProductItemComponent implements OnInit {
  isEdit: boolean = false;
  productitemform: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl(''),
    categories: new FormControl('')
  });;
  public onClose!: Subject<boolean>;
  modalDataPass: any;
  personId: number | undefined;
  submitted: boolean = false;
  loader: boolean = false;
  product!: ProductItem;
  constructor(private bsModalRef: BsModalRef, private modalService: BsModalService,
    private formBuilder: FormBuilder, private productService: ProductItemService,
    public options: ModalOptions) {
     
     }

  ngOnInit(): void {
    this.onClose = new Subject();
    this.modalDataPass = this.options.initialState;
    if (this.modalDataPass && this.modalDataPass != null) {
      if (this.modalDataPass.isEdit) {
        this.isEdit = true;
        this.updateEditInformation(this.modalDataPass.product);
      }
    }

  }

  onSubmit(value: any): void {
    this.submitted = true;
    if (this.productitemform.invalid) {
      return;
    }
    if(!this.isEdit){
      this.loader = true;
      this.productService.addProductItem(value).subscribe((res: any) => {
          // this.toastr.success('Added Successfully');
          this.cancel(true);
        },
        (error: Error) => {
          console.log(error);
          // this.toastr.error('Couldnot add callflow');
        }, () => { this.loader = false; });
    }else{
      this.loader = true;
      this.productService.updateProductItem(value.id, value).subscribe((res: any) => {
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

  get f() { return this.productitemform.controls; }

  updateEditInformation(product: ProductItem): void {
    this.productitemform = new FormGroup({
      id: new FormControl(product.id, Validators.required),
      name: new FormControl(product.name, Validators.required),
      description: new FormControl(product.description, Validators.required),
      categories: new FormControl(product.categories, Validators.required),
    });
  }

}
