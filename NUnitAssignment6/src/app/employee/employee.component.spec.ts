import { async, ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { Employee } from '../employee';

import { EmployeeComponent } from './employee.component';

describe('EmployeeComponent', () => {
  let component: EmployeeComponent;
  let fixture: ComponentFixture<EmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should get employees', (done) => {
    let employees;
    component.GetEmployees().then((data)=>{
      employees=data;
      expect(employees.length).toBeGreaterThanOrEqual(0);
    done();
    });

  });
  it('should get particular employee', fakeAsync(() => {
    let employee;
    component.GetEmployee(1).then((data)=>{
      employee=data;
    });
    tick();
    expect(employee!=undefined).toBeTruthy();

  }));
  it('should add particular employee', fakeAsync(() => {
    let employee=new Employee();
    employee.Id=5;
    employee.Name="rahul5";
    employee.Salary=30000;
    employee.Designation="SE";
    let message;
    component.AddEmployee(employee).then((data)=>{
      message=data;
    });
    tick();

      expect(message=="Added Successfully").toBeTruthy();

  }));
  it('should update particular employee', fakeAsync(() => {
    let employee=new Employee();
    employee.Id=1;
    employee.Name="rahul5";
    employee.Salary=40000;
    employee.Designation="SE";
    let message;
    component.UpdateEmployee(employee).then((data)=>{
      message=data;
    });
      tick();

      expect(message=="Updated Successfully").toBeTruthy();

  }));
  it('should delete particular employee', fakeAsync(() => {
    let message;
    component.DeleteEmployee(1).then((data)=>{
      message=data;
    });
    tick();
    expect(message=="Deleted Successfully").toBeTruthy();

  }));
});
