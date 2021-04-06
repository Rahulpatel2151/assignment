import { Component, OnInit } from '@angular/core';
import { Employee } from '../employee';
import { EmployeeService } from '../employee.service';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  constructor(private employeeService:EmployeeService) { }
  employees:Employee[]=[];
  ngOnInit(): void {
  }
  async GetEmployees():Promise<Employee[]>{
    return await this.employeeService.GetEmployees();
  }
  async GetEmployee(id:number):Promise<Employee>{
    return await this.employeeService.GetEmployee(id);
  }
  async AddEmployee(employee:Employee):Promise<string>{
    return await this.employeeService.AddEmployee(employee);
  }
  async DeleteEmployee(id:number):Promise<string>{
    return await this.employeeService.DeleteEmployee(id);
  }
  async UpdateEmployee(employee:Employee):Promise<string>{
    return await this.employeeService.UpdateEmployee(employee);
  }

}
