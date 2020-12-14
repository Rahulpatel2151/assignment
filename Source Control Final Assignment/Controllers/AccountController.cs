using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Source_Control_Final_Assignment.Models;
namespace Source_Control_Final_Assignment.Controllers
{
    public class AccountController : Controller
    {
        // GET: Users
        private SCFinalAssignmentEntities db = new SCFinalAssignmentEntities();
        [Authorize]
        public ActionResult Index()
        {
            string name=User.Identity.Name;
            System.Console.WriteLine("Findding:"+name);
            var model=db.Users.Where(e => e.Username == name).ToList();

            
            return View(model[0]);
        }
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(Members m)
        {
            using (var context = new SCFinalAssignmentEntities())
            {
                string hashedPassword = HashSHA1(m.password);
                bool isValid = context.Users.Any(x => x.Username == m.username && x.Password == hashedPassword );
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(m.username, false);  
                    return RedirectToAction("Index","Account");
                }
                ModelState.AddModelError("", "Invalid Username or Password");
            }
                return View();
        }

        public ActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(MemberRegistration m)
        {
            Users u = new Users()
            {
                Username = m.Username,
                Password = HashSHA1(m.Password),
                Email=m.Email,
                MobileNo=m.MobileNo,
                Fullname = m.Fullname,
                Designation = m.Designation,
                Salary = m.Salary,
                Age = m.Age
            };
            using (var context = new SCFinalAssignmentEntities())
            {
                context.Users.Add(u);
                context.SaveChanges();
                return RedirectToAction("Login", "Account");
            }

        }
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Users.Where(e => e.Username == User.Identity.Name).ToList();
            Users u = model[0];
            if (u.Id != id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (u== null)
            {
                return HttpNotFound();
            }
            MemberRegistration m = new MemberRegistration()
            {
                Id = u.Id,
                Username = u.Username,
                Password = u.Password,
                Email = u.Email,
                MobileNo = u.MobileNo,
                Fullname = u.Fullname,
                Designation = u.Designation,
                Salary = u.Salary,
                Age = u.Age
            };
            return View(m);
        }

       
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id,MemberRegistration m)
        {
            Users u = new Users()
            {
                Id=m.Id,
                Username = m.Username,
                Password = HashSHA1(m.Password),
                Email = m.Email,
                MobileNo = m.MobileNo,
                Fullname = m.Fullname,
                Designation = m.Designation,
                Salary = m.Salary,
                Age = m.Age
            };
            ViewBag.execute = "Not Executed";

            if (id==m.Id)
            {
                ViewBag.execute = "Executed";
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Index",u);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}