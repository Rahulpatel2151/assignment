import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormComponent } from './form.component';

describe('FormComponent', () => {
  let component: FormComponent;
  let fixture: ComponentFixture<FormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should be valid form', () => {
    component.userForm.controls['Name'].setValue("Rahul");
  component.userForm.controls['Email'].setValue("Rahul@gmail.com");
    expect(component.userForm.valid).toBeTruthy();
  });
    it('should be invalid form', () => {
      component.userForm.controls['Name'].setValue("");
    component.userForm.controls['Email'].setValue("");
      expect(component.userForm.valid).toBeFalsy();
    });
});
