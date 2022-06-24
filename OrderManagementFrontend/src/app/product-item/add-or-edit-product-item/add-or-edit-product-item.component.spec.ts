import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOrEditProductItemComponent } from './add-or-edit-product-item.component';

describe('AddOrEditProductItemComponent', () => {
  let component: AddOrEditProductItemComponent;
  let fixture: ComponentFixture<AddOrEditProductItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddOrEditProductItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddOrEditProductItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
