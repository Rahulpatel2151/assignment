import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultiplyComponent } from './multiply.component';

describe('MultiplyComponent', () => {
  let component: MultiplyComponent;
  let fixture: ComponentFixture<MultiplyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultiplyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MultiplyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should multiply', () => {
    let a=3;
    let b=5;
    expect(component.multiply(a,b)==15).toBeTruthy();
  });
  it('should not find factorial beacuse of negative number', () => {
    expect(component.factorial(-7)==0).toBeTruthy();
  });
});
