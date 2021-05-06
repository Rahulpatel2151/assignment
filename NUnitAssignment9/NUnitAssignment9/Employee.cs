using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitAssignment9
{
    public class ERP
    {
         List<Employee> employees = new List<Employee>()
        {
            new Employee(){Id=1,Name="abc1",Salary=40000,Designation="AP"},
            new Employee(){Id=2,Name="abc2",Salary=35000,Designation="SE"},
            new Employee(){Id=3,Name="abc3",Salary=50000,Designation="TL"},
            new Employee(){Id=4,Name="abc4",Salary=20000,Designation="SE"},
            new Employee(){Id=5,Name="abc5",Salary=25000,Designation="SE"},
            new Employee(){Id=6,Name="abc6",Salary=23000,Designation="SE"},
            new Employee(){Id=7,Name="abc7",Salary=20000,Designation="TL"},
            new Employee(){Id=8,Name="abc8",Salary=37000,Designation="AP"},
            new Employee(){Id=9,Name="abc9",Salary=22000,Designation="SE"},
            new Employee(){Id=10,Name="abc10",Salary=54000,Designation="TL"},
            new Employee(){Id=11,Name="abc11",Salary=36000,Designation="AP"},


        };
        public virtual List<Employee> GetEmployees()
        {
            return employees;
        }
        public  Employee GetEmployee(int id)
        {
            Employee employee = employees.Where(x => x.Id == id).FirstOrDefault();
            if (employee == null)
                throw new Exception("Employee not Found");
            return employee;
        }
        public virtual List<Employee> GetEmployeesByDesignation(string designation)
        {
            List<Employee> employee = employees.Where(x => x.Designation == designation).ToList();
            return employee;

        }
        public string GetEmployeeName(int id)
        {
            Employee employee = employees.Where(x => x.Id == id).FirstOrDefault();
            return employee.Name;
        }
        public virtual int TotalEmployee()
        {
            int count;
            count = GetEmployees().Count();
            return count;
        }
        public virtual Employee GetHighestPaidEmployee()
        {
            Employee employee = new Employee();
            employee = GetEmployees().OrderByDescending(x => x.Salary).FirstOrDefault();
            return employee;
        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Designation { get; set; }
    }
}
