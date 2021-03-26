using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.MVC.Models;
using HRM.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace HRM.MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly HRMMVCContext _context;

        public DepartmentController(HRMMVCContext context)
        {
            _context = context;
        }

        // GET: DepartmentViews
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Index()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                var response = await client.GetAsync("department");
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    var responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    IEnumerable<DepartmentView> departments = JsonConvert.DeserializeObject<IEnumerable<DepartmentView>>(responseAsConcreteType.Data.ToString());
                    return View(departments);
                }
                else
                {
                    return View();
                }
            }
        }

        // GET: DepartmentViews/Details/5
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DepartmentView department = await GetDepartment((int)id);
            return View(department);
        }

        // GET: DepartmentViews/Create
        [CustomFilters.CustomAuthorizeFilter]

        public IActionResult Create()
        {
            return View();
        }

        // POST: DepartmentViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Create([Bind("Name")] DepartmentView departmentView)
        {
           
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                string data = JsonConvert.SerializeObject(departmentView);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = await client.PostAsync("department", content);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    HttpResponsiveViewModel responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    if (responseAsConcreteType.StatusCode == 200)
                    {
                        return Redirect("/department/index");

                    }
                    else
                    {
                        ViewBag.Result = responseAsConcreteType.Result;
                        ViewBag.Message = responseAsConcreteType.Message;
                        return View(departmentView);
                    }
                }
                else
                {
                    return View();
                }
            }
        }

        // GET: DepartmentViews/Edit/5
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("Index");
            }
            DepartmentView department = await GetDepartment((int)id);
            
            return View(department);
        }

        // POST: DepartmentViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DepartmentView departmentView)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                string data = JsonConvert.SerializeObject(departmentView);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = await client.PutAsync("department/" + id, content);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    HttpResponsiveViewModel responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    if (responseAsConcreteType.StatusCode == 200)
                    {
                        return Redirect("/department/index");

                    }
                    else
                    {
                        ViewBag.Result = responseAsConcreteType.Result;
                        ViewBag.Message = responseAsConcreteType.Message;
                        return View(departmentView);
                    }
                }
                else
                {
                    return View();
                }
            }
        }

        // GET: DepartmentViews/Delete/5
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("Index");
            }
            DepartmentView employee = await GetDepartment((int)id);
            return View(employee);
        }

        // POST: DepartmentViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomFilters.CustomAuthorizeFilter]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {

                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = await client.DeleteAsync("department/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    HttpResponsiveViewModel responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    if (responseAsConcreteType.StatusCode == 200)
                    {
                        return Redirect("/department/index");

                    }
                    else
                    {

                        return Redirect("/department/index");
                    }
                }
                else
                {
                    return Redirect("/department/index");
                }
            }
        }

        public async Task<DepartmentView> GetDepartment(int id)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = await client.GetAsync("department/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    var responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    DepartmentView department = JsonConvert.DeserializeObject<DepartmentView>(responseAsConcreteType.Data.ToString());
                   
                    return department;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
