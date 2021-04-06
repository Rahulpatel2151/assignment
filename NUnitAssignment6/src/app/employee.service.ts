import { Injectable } from '@angular/core';
import { Employee } from './employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  employeeList:Employee[]=[];
  constructor() {
    this.employeeList=[
      {Id:1,Name:"Rahul1",Salary:30000,Designation:"SE"},
      {Id:2,Name:"Rahul2",Salary:35000,Designation:"SE"},
      {Id:3,Name:"Rahul3",Salary:40000,Designation:"SE"},
      {Id:4,Name:"Rahul4",Salary:45000,Designation:"PM"}
    ];
  }
  async GetEmployees():Promise<Employee[]>{
    return Promise.resolve(this.employeeList);
  }
  async GetEmployee(id:number):Promise<Employee>{
    return Promise.resolve(this.employeeList.find(x=>x.Id==id));
  }
  async AddEmployee(employee:Employee):Promise<string>{
    this.employeeList.push(employee);
    return Promise.resolve("Added Successfully");
  }
  async UpdateEmployee(employee:Employee){
    let index=this.employeeList.findIndex(x=>x.Id==employee.Id);
    this.employeeList[index].Name=employee.Name;
    this.employeeList[index].Salary=employee.Salary;
    this.employeeList[index].Designation=employee.Designation;
    return Promise.resolve("Updated Successfully");
  }
  async DeleteEmployee(id:number):Promise<string>{
    this.employeeList=this.employeeList.filter(x=>x.Id==id);
    return Promise.resolve("Deleted Successfully");
  }
}
