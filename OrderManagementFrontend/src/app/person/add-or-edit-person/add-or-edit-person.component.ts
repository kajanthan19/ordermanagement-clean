import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
import { PersonService } from 'src/app/services/person.service';
import { Person } from '../person';

@Component({
  selector: 'app-add-or-edit-person',
  templateUrl: './add-or-edit-person.component.html',
  styleUrls: ['./add-or-edit-person.component.scss']
})
export class AddOrEditPersonComponent implements OnInit {

  isEdit: boolean = false;
  personform: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required)
  });;
  public onClose!: Subject<boolean>;
  modalDataPass: any;
  personId: number | undefined;
  submitted: boolean = false;
  loader: boolean = false;
  person!: Person;
  constructor(private bsModalRef: BsModalRef, private modalService: BsModalService,
     private formBuilder: FormBuilder, private personService: PersonService,
     public options: ModalOptions) {
      
      }

  ngOnInit(): void {
    this.onClose = new Subject();
    this.modalDataPass = this.options.initialState;
    if (this.modalDataPass && this.modalDataPass != null) {
      if (this.modalDataPass.isEdit) {
        this.isEdit = true;
        this.person = this.modalDataPass.person;
        this.updateEditInformation(this.person);
      }
    }

  }

  onSubmit(value: any): void {
    this.submitted = true;
    if (this.personform.invalid) {
      return;
    }
    if(!this.isEdit){
      this.loader = true;
      this.personService.addPerson(value).subscribe((res: any) => {
          // this.toastr.success('Added Successfully');
          this.cancel(true);
        },
        (error: Error) => {
          console.log(error);
          // this.toastr.error('Couldnot add callflow');
        }, () => { this.loader = false; });
    }else {
      this.loader = true;
      this.personService.updatePerson(value.id, value).subscribe((res: any) => {
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

  updateEditInformation(person: Person): void {
    this.personform = new FormGroup({
      id: new FormControl(person.id, Validators.required),
      name: new FormControl(person.name, Validators.required),
      email: new FormControl(person.email, Validators.required),
    });
  }

}
