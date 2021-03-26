using HRM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.DAL.Repository
{
    public interface IEmployeeRepository
    {
        EmployeeView GetEmployee(int id);
        IEnumerable<EmployeeView> GetEmployees();
        string PutEmployee(int id, EmployeeView employee);
        string PostEmployee(EmployeeView employee);
        string DeleteEmployee(int id);
        IEnumerable<DepartmentView> GetDepartments();
        DepartmentView GetDepartment(int id);
        string DeleteDepartment(int id);
        string PostDepartment(DepartmentView department);
        string PutDepartment(int id, DepartmentView department);
    }
}
