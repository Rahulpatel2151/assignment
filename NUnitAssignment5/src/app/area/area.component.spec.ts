import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MockAreaService } from '../Mock/mockarea';

import { AreaComponent } from './area.component';

describe('AreaComponent', () => {
  let component: AreaComponent;
  let areaService:MockAreaService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AreaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    areaService=new MockAreaService();
    component=new AreaComponent(areaService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should return circle area', () => {
    //Mock service Method will print 'Mock' in console
    let circleArea=component.Circle(5);
    expect(circleArea).toEqual(78.5);
  });
  it('should return square area', () => {
      //Mock service Method will print 'Mock' in console
    let circleArea=component.Square(5);
    expect(circleArea).toEqual(25);
  });
  it('should not equal the returned square area', () => {
    //Mock service Method will print 'Mock' in console
    let circleArea=component.Square(5);
    expect(circleArea==27).toBeFalsy();
  });
  it('should return rectangle area', () => {
    //Mock service Method will print 'Mock' in console
    let circleArea=component.Rectangle(5,6);
    expect(circleArea).toEqual(30);
  });
  it('should not equal returned rectangle area', () => {
    //Mock service Method will print 'Mock' in console
    let circleArea=component.Rectangle(5,6);
    expect(circleArea==32).toBeFalsy();
  });
});
