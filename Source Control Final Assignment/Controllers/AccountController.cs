using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
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
            Users u;
            try
            {
                u = model[0];
                return View(model[0]);
            }
            catch
            {
                return RedirectToAction("Logout");
            }
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
            if (!ModelState.IsValid)
            {
                return View(m);
            }
            bool isValid = db.Users.Any(x => x.Username == m.Username && x.Username != User.Identity.Name);
            if (!isValid)
            {
                
                string filepath = "/UploadedFiles/default.png";
                if (m.profilephoto != null)
                {
                    
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(m.profilephoto.FileName));
                    filepath = "/UploadedFiles/" + Path.GetFileName(m.profilephoto.FileName);
                    m.profilephoto.SaveAs(path);
                }
                Users u = new Users()
                {
                    Username = m.Username,
                    Password = HashSHA1(m.Password),
                    Email = m.Email,
                    MobileNo = m.MobileNo,
                    Fullname = m.Fullname,
                    Designation = m.Designation,
                    Salary = m.Salary,
                    Age = m.Age,
                    GraduationDate=m.GraduationDate,
                    profilephoto = filepath
                };
                using (var context = new SCFinalAssignmentEntities())
                {
                    context.Users.Add(u);
                    context.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("Username", "Username already exists!!");
                return View(m);
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
            Users u;
            try
            {
                 u = model[0];
            }
            catch
            {
                return RedirectToAction("Logout");
            }
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
                Age = u.Age,
                GraduationDate=u.GraduationDate
            };
            return View(m);
        }

       
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id,MemberRegistration m)
        {
            ModelState["Password"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                return View(m);
            }
            
            bool isValid = db.Users.Any(x => x.Username == m.Username && x.Username != User.Identity.Name );
            if (!isValid)
            {
                var model = db.Users.Where(e => e.Username == User.Identity.Name).ToList();
                Users u = model[0];
                if (m.profilephoto != null)
                {
                    
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(m.profilephoto.FileName));
                    string filepath = "/UploadedFiles/" + Path.GetFileName(m.profilephoto.FileName);
                    m.profilephoto.SaveAs(path);
                    u.profilephoto = filepath;
                }
                u.Id = m.Id;
                u.Username = m.Username;
                u.Email = m.Email;
                u.MobileNo = m.MobileNo;
                u.Fullname = m.Fullname;
                u.Designation = m.Designation;
                u.Salary = m.Salary;
                u.Age = m.Age;
                u.GraduationDate = m.GraduationDate;
                if (id == m.Id)
                {
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Logout");
            }
            else
            {
                ModelState.AddModelError("Username", "Username already exists!!");
                return View(m);
            }
        }
        public ActionResult ChangePassword(int? id)
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
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(new ChangePassword());
        }
        [HttpPost]
        public ActionResult ChangePassword(int? id,ChangePassword c)
        {
            if (c == null)
            {
                return HttpNotFound();
            }
            string arrived = HashSHA1(c.currentPassword);
            bool isValid = db.Users.Any(x => x.Username == User.Identity.Name&& x.Password == arrived);
            if (isValid)
            {
                var model = db.Users.Where(e => e.Username == User.Identity.Name).ToList();
                Users u = model[0];
                if (u.Id != id)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }
                u.Password = HashSHA1(c.NewPassword);
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return View("Index", u);
            }
            else
            {
                ModelState.AddModelError("currentPassword", "Password not match with the records");
                return View(c);
            }

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