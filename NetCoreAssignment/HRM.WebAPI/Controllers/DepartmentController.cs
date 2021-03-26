using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.BAL;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public DepartmentController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        // GET: api/Department
        [HttpGet]
        public HttpResponsiveViewModel GetDepartments()
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();
            try
            {
                IEnumerable<DepartmentView> departmentlist = _employeeManager.GetDepartments();

                if (departmentlist == null)
                {
                    model.Data = null;
                    model.Result = false;
                    model.StatusCode = 404;
                    model.Message = "department not found";
                    return model;
                }
                else
                {
                    model.Data = departmentlist;
                    model.Result = true;
                    model.StatusCode = 200;
                    model.Message = "department Data fetched";
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

        // GET: api/Department/5
        [HttpGet("{id}")]
        public HttpResponsiveViewModel GetDepartment(int id)
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();
            try
            {
                DepartmentView department = _employeeManager.GetDepartment(id);

                if (department == null)
                {
                    model.Data = null;
                    model.Result = false;
                    model.StatusCode = 404;
                    model.Message = "department not found";
                    return model;
                }
                else
                {
                    model.Data = department;
                    model.Result = true;
                    model.StatusCode = 200;
                    model.Message = "department Data fetched";
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

        // PUT: api/Department/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public HttpResponsiveViewModel PutDepartment(int id, DepartmentView department)
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();

            if (id != department.Id)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 400;
                model.Message = "Invalid request";
                return model;
            }
            DepartmentView findEmployee = _employeeManager.GetDepartment(id);

            if (findEmployee == null)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 404;
                model.Message = "department not found";
                return model;
            }
            try
            {
                string result = _employeeManager.PutDepartment(id, department);
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

        // POST: api/Department
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public HttpResponsiveViewModel PostDepartment(DepartmentView department)
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();

            if (!ModelState.IsValid)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 400;
                model.Message = "Invalid Data";
                return model;
            }

     
            string result = _employeeManager.PostDepartment(department);
            if (result.Contains("error occured!!!"))
            {
                model.Data = result;
                model.Result = false;
                model.StatusCode = 500;
                model.Message = "data not added";
                return model;
            }
            model.Data = result;
            model.Result = true;
            model.StatusCode = 200;
            model.Message = "data created";
            return model;
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public HttpResponsiveViewModel DeleteDepartment(int id)
        {
            HttpResponsiveViewModel model = new HttpResponsiveViewModel();
            DepartmentView findEmployee = _employeeManager.GetDepartment(id);

            if (findEmployee == null)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 404;
                model.Message = "department not found";
                return model;
            }
            try
            {
                model.Data = _employeeManager.DeleteDepartment(id);
                model.Result = true;
                model.StatusCode = 200;
                model.Message = "department deleted";
                return model;
            }
            catch (Exception e)
            {
                model.Data = null;
                model.Result = false;
                model.StatusCode = 500;
                model.Message = "Error occured!!!! " + e.Message;
                return model;
            }
        }

     
    }
}
