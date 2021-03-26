using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRM.Models;
using HRM.BAL;
using Microsoft.AspNetCore.Authorization;

namespace HRM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        // GET: api/Employee
        [HttpGet]
        public  HttpResponsiveViewModel GetEmployees()
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();
            try
            {
                IEnumerable<EmployeeView> employeelist = _employeeManager.GetEmployees();

                if (employeelist == null)
                {
                    model.Data = null;
                    model.Result = false;
                    model.StatusCode = 404;
                    model.Message = "Employees not found";
                    return model;
                }
                else
                {
                    model.Data = employeelist;
                    model.Result = true;
                    model.StatusCode = 200;
                    model.Message = "Employees Data fetched";
                    return model;
                }
            }
            catch (Exception e)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 500;
                model.Message = e.Message;
                return model;
            }
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public  HttpResponsiveViewModel GetEmployee(int id)
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();
            try
            {
                EmployeeView employee = _employeeManager.GetEmployee(id);

                if (employee == null)
                {
                    model.Data = null;
                    model.Result = false;
                    model.StatusCode = 404;
                    model.Message = "Employee not found";
                    return model;
                }
                else
                {
                    model.Data = employee;
                    model.Result = true;
                    model.StatusCode = 200;
                    model.Message = "Employee Data fetched";
                    return model;
                }
            }
            catch(Exception e)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 500;
                model.Message = e.Message;
                return model;
            }
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public  HttpResponsiveViewModel PutEmployee(int id, EmployeeView employee)
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();

            if (id != employee.Id)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 400;
                model.Message = "Invalid request";
                return model;
            }
            EmployeeView findEmployee = _employeeManager.GetEmployee(id);

            if (findEmployee == null)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 404;
                model.Message = "Employee not found";
                return model;
            }
            try
            {
                string result= _employeeManager.PutEmployee(id,employee);
                if(result.Contains("error occured!!!"))
                {
                    model.Data = result;
                    model.Result = false;
                    model.StatusCode = 500;
                    model.Message = "data not updated";
                    return model;
                }
                model.Data = result;
                model.Result = true;
                model.StatusCode = 200;
                model.Message = "data updated";
                return model;
            }
            catch (Exception e)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 500;
                model.Message = e.Message;
                return model;
            }

        }

        // POST: api/Employee
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public HttpResponsiveViewModel PostEmployee(EmployeeView employee)
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();

            if (!ModelState.IsValid)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 400;
                model.Message ="Invalid Data";
                return model;
            }
         
            string result = _employeeManager.PostEmployee(employee); ;
            if (result.Contains("error occured!!!"))
            {
                model.Data = result;
                model.Result = false;
                model.StatusCode = 500;
                model.Message = "data not updated";
                return model;
            }
            model.Data = result;
            model.Result = true;
            model.StatusCode = 200;
            model.Message = "data created";
            return model;
           
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public HttpResponsiveViewModel DeleteEmployee(int id)
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();
            EmployeeView findEmployee = _employeeManager.GetEmployee(id);

            if (findEmployee == null)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 404;
                model.Message = "Employee not found";
                return model;
            }
            try
            {
                model.Data = _employeeManager.DeleteEmployee(id);
                model.Result = true;
                model.StatusCode = 200;
                model.Message = "Employee deleted";
                return model;
            }catch(Exception e)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 500;
                model.Message = "Error occured!!!! "+e.Message;
                return model;
            }
        }

       
    }
}
