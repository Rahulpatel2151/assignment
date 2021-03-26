using System;
using System.Collections.Generic;
using System.Text;
using HRM.Models;
namespace HRM.BAL
{
    public interface IEmployeeManager
    {
        IEnumerable<EmployeeView> GetEmployees();
        EmployeeView GetEmployee(int id);
        string PutEmployee(int id, EmployeeView employee);
        string PostEmployee(EmployeeView employee);
        string DeleteEmployee(int id);
        IEnumerable<DepartmentView> GetDepartments();
        DepartmentView GetDepartment(int id);
        string PutDepartment(int id, DepartmentView department);
        string PostDepartment(DepartmentView department);
        string DeleteDepartment(int id);
    }
}
