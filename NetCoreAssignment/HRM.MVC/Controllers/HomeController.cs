using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HRM.MVC.Models;
using System.Net.Http;
using HRM.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace HRM.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [CustomFilters.CustomAuthorizeFilter]
        public async Task<IActionResult> Index()
        {
            UserModel user = await GetUser();
            ViewBag.Name = user.Username;
            ViewBag.Email = user.Email;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<UserModel> GetUser()
        {
            UserModel user = new UserModel();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {

                client.BaseAddress = new Uri("https://localhost:44334/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                HttpResponseMessage response = await client.GetAsync("account/getuser");
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    HttpResponsiveViewModel responseAsConcreteType = JsonConvert.DeserializeObject<HttpResponsiveViewModel>(readTask);
                    if (responseAsConcreteType.StatusCode == 200)
                    {
                        user = JsonConvert.DeserializeObject<UserModel>(responseAsConcreteType.Data.ToString());
                        return user;
                    }
                    else
                    {
                        return user;
                    }
                }
                else
                {
                    return user;
                }

            }
        }

    }
}
