import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StringComponent } from './string.component';

describe('StringComponent', () => {
  let component: StringComponent;
  let fixture: ComponentFixture<StringComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StringComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should convert lowercase', () => {
    fixture = TestBed.createComponent(StringComponent);
    component = fixture.componentInstance;
    let s:string="RAHUL";
    expect(component.lowercase(s)).toEqual("rahul");
  });
  it('should not convert uppercase', () => {
    fixture = TestBed.createComponent(StringComponent);
    component = fixture.componentInstance;
    let s:string="rahul";
    expect(component.uppercase(s)=="Rahul").toBeFalsy();
  })
});
