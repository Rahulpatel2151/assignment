using System;
using System.Collections.Generic;
using System.Text;
using HRM.DAL.Repository;
using HRM.Models;

namespace HRM.BAL
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public string DeleteDepartment(int id)
        {
            return _employeeRepository.DeleteDepartment(id);
        }

        public string DeleteEmployee(int id)
        {
            return _employeeRepository.DeleteEmployee(id);
        }

        public DepartmentView GetDepartment(int id)
        {
            return _employeeRepository.GetDepartment(id);
        }

        public IEnumerable<DepartmentView> GetDepartments()
        {
            return _employeeRepository.GetDepartments();
        }

        public EmployeeView GetEmployee(int id)
        {
           return _employeeRepository.GetEmployee(id);
        }

        public IEnumerable<EmployeeView> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        public string PostDepartment(DepartmentView department)
        {
            return _employeeRepository.PostDepartment(department);
        }

        public string PostEmployee(EmployeeView employee)
        {
            return _employeeRepository.PostEmployee(employee);
        }

        public string PutDepartment(int id, DepartmentView department)
        {
            return _employeeRepository.PutDepartment(id, department);
        }

        public string PutEmployee(int id, EmployeeView employee)
        {
            return _employeeRepository.PutEmployee(id,employee);
        }

       
    }
}
