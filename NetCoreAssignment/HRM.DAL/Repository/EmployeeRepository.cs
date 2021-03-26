using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using HRM.DAL.Database;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.DAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMapper _mapper;
        EmployeeDbContext db = new EmployeeDbContext();
        public EmployeeRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public EmployeeView GetEmployee(int id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                return _mapper.Map<EmployeeView>(employee);
            }
            catch
            {
                return null;
            }

        }

        public IEnumerable<EmployeeView> GetEmployees()
        {
            try
            {
                IEnumerable<Employee> employees = db.Employees.ToList();
                IEnumerable<EmployeeView> employeeViews = employees.Select(e => _mapper.Map<EmployeeView>(e));
                return employeeViews;
            }
            catch
            {
                return null;
            }
        }

        public string PutEmployee(int id, EmployeeView employee)
        {
            try
            {
                Employee employee1=db.Employees.Find(id);
                employee1.Name = employee.Name;
                employee1.Phone = employee.Phone;
                employee1.Salary = employee.Salary;
                employee1.Email = employee.Email;
                employee1.Manager = employee.Manager;
                employee1.IsManager = employee.IsManager;
                employee1.DepartmentId = employee.DepartmentId;
                db.Entry(employee1).State = EntityState.Modified;
                db.SaveChanges();
                return "Employee data updated successfully";
            }
            catch (Exception e)
            {
                return "error occured!!!"+e.Message;
            }
        }
        public string PostEmployee(EmployeeView employee)
        {
            try
            {
                Employee employee1 = _mapper.Map<Employee>(employee);
                db.Employees.Add(employee1);
                db.SaveChanges();
                return "Employee data saved successfully";
            }
            catch
            {
                return "Employee not added";
            }
           
        }

        public string DeleteEmployee(int id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
                db.SaveChanges();
                return "Employee data deleted successfully";
            }
            catch (Exception e)
            {
                return "error occured!!!"+e.Message;
            }
        }
        public IEnumerable<DepartmentView> GetDepartments()
        {
            try
            {
                IEnumerable<Department> departments = db.Departments.ToList();
                IEnumerable<DepartmentView> departmentViews = departments.Select(e => _mapper.Map<DepartmentView>(e));
                return departmentViews;
            }
            catch
            {
                return null;
            }
        }
        public DepartmentView GetDepartment(int id)
        {
            try
            {
                Department department = db.Departments.Find(id);
                DepartmentView departmentView = _mapper.Map<DepartmentView>(department);
                return departmentView;
            }
            catch
            {
                return null;
            }
        }
        public string PostDepartment(DepartmentView departmentView)
        {
            try
            {
                Department department = _mapper.Map<Department>(departmentView);
                db.Departments.Add(department);
                db.SaveChanges();

                return "Department added successfully";
            }
            catch(Exception e)
            {
                return "error occured!!!" + e.Message;
            }
        }

        public string DeleteDepartment(int id)
        {
            try
            {
                IEnumerable<Employee> employees= db.Employees.Where(x=>x.DepartmentId==id);
                db.Employees.RemoveRange(employees);
                Department department = db.Departments.Find(id);
                db.Departments.Remove(department);
                db.SaveChanges();
                return "department data deleted successfully";
            }
            catch (Exception e)
            {
                return "error occured!!!" + e.Message;
            }
        }

        public string PutDepartment(int id, DepartmentView department)
        {
            try
            {
                Department department1 = db.Departments.Find(id);
                department1.Name = department.Name;
                
                db.Entry(department1).State = EntityState.Modified;
                db.SaveChanges();
                return "department data updated successfully";
            }
            catch (Exception e)
            {
                return "error occured!!!" + e.Message;
            }
        }
    }
}
