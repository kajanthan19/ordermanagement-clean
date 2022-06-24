import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';
import { PersonService } from '../services/person.service';
import { DeleteComponent } from '../shared/delete/delete.component';
import { AddOrEditPersonComponent } from './add-or-edit-person/add-or-edit-person.component';
import { Person } from './person';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.scss']
})
export class PersonComponent implements OnInit {

  public onClose!: Subject<boolean>;
  public modalRef!: BsModalRef;
  personlist: Person []= [];
  constructor(private modalService: BsModalService, private personService: PersonService) { }

  ngOnInit(): void {
    this.onClose = new Subject();
    this.getCallPersonList();
  }

  getCallPersonList(){
    this.personService.getPersonList().subscribe((res: any) => {
      if(res!=null){
        this.personlist = res;
      } 
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
    this.modalRef = this.modalService.show(AddOrEditPersonComponent, Object.assign({}, modalConfig, {class: 'modal-md', initialState}));
    this.modalRef.content.onClose.subscribe((result: boolean) => {
      if (result) {
        this.getCallPersonList();
      }
    });
  }


  onEditRow(person: Person): void {
    const initialState = {isEdit: true, person: person };
    let modalConfig = { animated: true, keyboard: true, backdrop: true, ignoreBackdropClick: false };
    this.modalRef = this.modalService.show(AddOrEditPersonComponent, Object.assign({}, modalConfig, {class: 'modal-md', initialState}));
    this.modalRef.content.onClose.subscribe((result: boolean) => {
      if (result) {
        this.getCallPersonList();
      }
    });
  }

  onDeleteRow(person: Person, index: number): void {
    const initialState = {
      // backdrop: 'static',
      keyboard: false,
      backdrop: true,
      ignoreBackdropClick: true,
    };

    this.modalRef = this.modalService.show(DeleteComponent, initialState);
    this.modalRef.content.onClose.subscribe((result: any) => {
      if (result.Action === 'DELETE') {
        this.delete(person, index);
      } else {

      }
    });
  }

  delete(person: Person, index: number): void{
     //this.loader = true;
     this.personService.deletePerson(person.id).subscribe((res: any) => {
      // this.toastr.success('Added Successfully');
      this.personlist.splice(index, 1)
    },
    (error: Error) => {
      console.log(error);
      // this.toastr.error('Couldnot add callflow');
    }, () => { //this.loader = false; 
    });
  }

}
