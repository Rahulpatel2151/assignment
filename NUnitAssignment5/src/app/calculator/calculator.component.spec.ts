import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MockCalculator } from '../Mock/mock-calculator';

import { CalculatorComponent } from './calculator.component';

describe('CalculatorComponent', () => {
  let component: CalculatorComponent;
  let Utilityclass:MockCalculator;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CalculatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    Utilityclass= new MockCalculator();
    component = new CalculatorComponent(Utilityclass);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should add', () => {
    //Mock class Method will print 'Mock' in console
    let total=component.Add(2,3);
    expect(total==5).toBeTruthy();
  });
  it('should not add ', () => {
    //Mock class Method will print 'Mock' in console
    let total=component.Add(2,3);
    expect(total==9).toBeFalsy();
  });
  it('should substract', () => {
      //Mock class Method will print 'Mock' in console
    let total=component.Substract(5,2);
    expect(total==3).toBeTruthy();
  });
  it('should multiply', () => {
    //Mock class Method will print 'Mock' in console
  let total=component.Multiply(5,2);
  expect(total==10).toBeTruthy();
});
it('should divide', () => {
  //Mock class Method will print 'Mock' in console
let total=component.Divide(10,2);
expect(total==5).toBeTruthy();
});
});
