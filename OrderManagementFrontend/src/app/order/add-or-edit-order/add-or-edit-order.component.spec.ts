import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOrEditOrderComponent } from './add-or-edit-order.component';

describe('AddOrEditOrderComponent', () => {
  let component: AddOrEditOrderComponent;
  let fixture: ComponentFixture<AddOrEditOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddOrEditOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddOrEditOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
