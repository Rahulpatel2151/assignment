using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.MVC.Models;
using HRM.Models;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace HRM.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly HRMMVCContext _context;
        ILogger _logger;

        public AdminController(HRMMVCContext context, ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                HttpResponseMessage response = await client.PostAsync("account/loginuser", content);
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    HttpResponsiveViewModel responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    if (responseAsConcreteType.StatusCode == 200)
                    {
                        HttpContext.Session.SetString("token",(string)responseAsConcreteType.Data);
                        HttpContext.Session.SetString("isAuthenticated","true");

                        return Redirect("/Home/index");

                    }
                    else
                    {
                        ViewBag.Result = responseAsConcreteType.Result;
                        ViewBag.Message = responseAsConcreteType.Message;
                        _logger.LogError(responseAsConcreteType.Message);

                        return View(user);
                    }
                }
                else
                {
                    return View();
                }
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("login");
        }
      
    }

}
