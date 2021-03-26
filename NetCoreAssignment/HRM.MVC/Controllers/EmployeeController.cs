using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HRM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HRM.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        ILogger _logger;
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }
        [ResponseCache(Duration = 1)]
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Index()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client =new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                var response =await client.GetAsync("employee");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Employee data fetched");
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    var responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    IEnumerable<EmployeeView> employees = JsonConvert.DeserializeObject<IEnumerable<EmployeeView>>(responseAsConcreteType.Data.ToString());
                    return View(employees);
                }
                else
                {
                    return View();
                }
            }
        }
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Details(int? id)
        {
            EmployeeView employee = await GetEmployee((int)id);
            IEnumerable<DepartmentView> departments = await GetDepartments();
            ViewBag.departmentName = departments.Where(x => x.Id == employee.DepartmentId).FirstOrDefault().Name;
            return View(employee);
        }
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Create()
        {
            IEnumerable<DepartmentView> departments =await GetDepartments();
            ViewBag.departments = new SelectList(departments, "Id", "Name");
            return View();
        }
        [HttpPost]
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Create(EmployeeView employee)
        {
            IEnumerable<DepartmentView> departments = await GetDepartments();
            ViewBag.departments = new SelectList(departments, "Id", "Name");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                string data = JsonConvert.SerializeObject(employee);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                client.BaseAddress = new Uri("https://localhost:44334/api/");
                HttpResponseMessage response = await client.PostAsync("employee",content);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    HttpResponsiveViewModel responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    if (responseAsConcreteType.StatusCode == 200)
                    {
                        return Redirect("index");

                    }
                    else
                    {
                        ViewBag.Result = responseAsConcreteType.Result;
                        ViewBag.Message = responseAsConcreteType.Message;
                        _logger.LogError(responseAsConcreteType.Message);

                        return View(employee);
                    }
                }
                else
                {
                    return View();
                }
            }
        }
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("Index");
            }
            EmployeeView employee = await GetEmployee((int)id);

            IEnumerable<DepartmentView> departments = await GetDepartments();
            ViewBag.departments = new SelectList(departments, "Id", "Name");
            return View(employee);
        }
        [HttpPost]
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Edit(int? id,EmployeeView employee)
        {
            IEnumerable<DepartmentView> departments = await GetDepartments();
            ViewBag.departments = new SelectList(departments, "Id", "Name");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                string data = JsonConvert.SerializeObject(employee);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = await client.PutAsync("employee/"+id, content);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    HttpResponsiveViewModel responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    if (responseAsConcreteType.StatusCode == 200)
                    {
                        return Redirect("/Employee/index");

                    }
                    else
                    {
                        ViewBag.Result = responseAsConcreteType.Result;
                        ViewBag.Message = responseAsConcreteType.Message;
                        _logger.LogError(responseAsConcreteType.Message);

                        return View(employee);
                    }
                }
                else
                {
                    return View();
                }
            }
        }
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> delete(int? id)
        {
            if (id == null)
            {
                return Redirect("Index");
            }
            EmployeeView employee = await GetEmployee((int)id);
            IEnumerable<DepartmentView> departments = await GetDepartments();
            ViewBag.departments = new SelectList(departments, "Id", "Name");
            
            return View(employee);
        }
        [HttpPost]
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> deleteConfirmed(int? id)
        {
            IEnumerable<DepartmentView> departments = await GetDepartments();
            ViewBag.departments = new SelectList(departments, "Id", "Name");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
              
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = await client.DeleteAsync("employee/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    HttpResponsiveViewModel responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    if (responseAsConcreteType.StatusCode == 200)
                    {
                        return Redirect("/Employee/index");

                    }
                    else
                    {
                        _logger.LogError(responseAsConcreteType.Message);

                        return Redirect("/Employee/index");
                    }
                }
                else
                {
                    return Redirect("/Employee/index");
                }
            }
        }

        public async  Task<IEnumerable<DepartmentView>> GetDepartments()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = await client.GetAsync("department");
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    var responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    IEnumerable<DepartmentView> departments = JsonConvert.DeserializeObject<IEnumerable<DepartmentView>>(responseAsConcreteType.Data.ToString());
                    return departments;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<EmployeeView> GetEmployee(int id)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = await client.GetAsync("employee/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    var responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    EmployeeView employee = JsonConvert.DeserializeObject<EmployeeView>(responseAsConcreteType.Data.ToString());
                    IEnumerable<DepartmentView> departments = await GetDepartments();
                    ViewBag.departmentName = departments.Where(x => x.Id == employee.DepartmentId).FirstOrDefault().Name;
                    return employee;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
