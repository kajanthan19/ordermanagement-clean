import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOrEditPersonComponent } from './add-or-edit-person.component';

describe('AddOrEditPersonComponent', () => {
  let component: AddOrEditPersonComponent;
  let fixture: ComponentFixture<AddOrEditPersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddOrEditPersonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddOrEditPersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
