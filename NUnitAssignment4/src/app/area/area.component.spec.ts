import { analyzeAndValidateNgModules } from '@angular/compiler';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Area } from '../area';

import { AreaComponent } from './area.component';

describe('AreaComponent', () => {
  let component: AreaComponent;
  let areaClass:Area;
  let spy:any;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AreaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    areaClass=new Area();
   component=new AreaComponent(areaClass);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should return circle area', () => {
    spy = spyOn(areaClass, 'Circle').and.returnValue(78.5);
    let circleArea=component.Circle(5);
    expect(circleArea).toEqual(78.5);
  });
  it('should return square area', () => {
    spy = spyOn(areaClass, 'Square').and.returnValue(25);
    let circleArea=component.Square(5);
    expect(circleArea).toEqual(25);
  });
  it('should not equal the returned square area', () => {
    spy = spyOn(areaClass, 'Square').and.returnValue(25);
    let circleArea=component.Square(5);
    expect(circleArea==27).toBeFalsy();
  });
  it('should return rectangle area', () => {
    spy = spyOn(areaClass, 'Rectangle').and.returnValue(30);
    let circleArea=component.Rectangle(5,6);
    expect(circleArea).toEqual(30);
  });
  it('should not equal returned rectangle area', () => {
    spy = spyOn(areaClass, 'Rectangle').and.returnValue(30);
    let circleArea=component.Rectangle(5,6);
    expect(circleArea==32).toBeFalsy();
  });
});
