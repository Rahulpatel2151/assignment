using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SourceControlAssignment1.Models;
namespace SourceControlAssignment1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegistrationData(Employee emp)
        {
            if (ModelState.IsValid)
            {
                ViewBag.result = "Data complete";
            }
            else
            {
                ViewBag.result = "Data incomplete";

            }
            return View("Index");
        }


    }
}