using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitAssignment8
{
    public static class ERP
    {
        static List<Employee> employees = new List<Employee>()
        {
            new Employee(){Id=1,Name="abc1",Salary=40000,Designation="AP"},
            new Employee(){Id=2,Name="abc2",Salary=35000,Designation="SE"},
            new Employee(){Id=3,Name="abc3",Salary=50000,Designation="TL"},
            new Employee(){Id=4,Name="abc4",Salary=20000,Designation="SE"},

        };
        public static List<Employee> GetEmployees()
        {
            return employees;
        }
        public static Employee GetEmployee(int id)
        {
            Employee employee = employees.Where(x => x.Id == id).FirstOrDefault();
            if (employee == null)
                throw new Exception("Employee not Found");
            return employee;
        }
        public static List<Employee> GetEmployeesByDesignation(string designation)
        {
            List<Employee> employee = employees.Where(x => x.Designation == designation).ToList();
            return employee;

        }
        public static string GetEmployeeName(int id)
        {
            Employee employee = employees.Where(x => x.Id == id).FirstOrDefault();
            return employee.Name;
        }
        public static  int TotalEmployee()
        {
            int count;
            count = GetEmployees().Count();
            return count;
        }
        public static  Employee GetHighestPaidEmployee()
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
