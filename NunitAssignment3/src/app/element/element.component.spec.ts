import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ElementComponent } from './element.component';

describe('ElementComponent', () => {
  let component: ElementComponent;
  let fixture: ComponentFixture<ElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should compare salutation', () => {
    const fixture = TestBed.createComponent(ElementComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('#salutation').textContent).toContain('Hello Rahul');
  });
  it('salary should be greater than 50000', () => {
    const fixture = TestBed.createComponent(ElementComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(parseInt(compiled.querySelector('#salary').textContent)>50000).toBeFalsy();
  });
});
