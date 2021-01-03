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
using PreJoiningFinalAssignment.Models;
using log4net;
namespace PreJoiningFinalAssignment.Controllers
{
    public class AccountController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccountController));
        // GET: Users
        private ProductsdbEntities db = new ProductsdbEntities();
        [Authorize]
        public ActionResult Index()
        {
            string name = User.Identity.Name;
            var model = db.Users.Where(e => e.Email == name).ToList();//find user
            Users u;
            try
            {
                u = model[0];
                ViewBag.Name = u.Name;//for welcome
                ViewBag.Id = u.Id;
                return View();
            }
            catch (Exception e)
            {
                Log.Error("Invalid Credentials", e);
                return RedirectToAction("Index");
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
        public ActionResult Login(Login m)
        {
            using (var context = new ProductsdbEntities())
            {
                string hashedPassword = HashSHA1(m.Password);//hashing
                try
                {
                    bool isValid = context.Users.Any(x => x.Email == m.Email && x.Password == hashedPassword);
                    if (isValid)
                    {
                        FormsAuthentication.SetAuthCookie(m.Email, false);
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username or Password");
                        return View();
                    }
                }
                catch (Exception e)
                {
                    Log.Debug("Login Failed", e);
                }
                return View();
            }
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
        public ActionResult SignUp(UserRegistration m)
        {
            if (!ModelState.IsValid)
            {
                return View(m);
            }
            bool isValid = db.Users.Any(x => x.Email == m.Email && x.Email != User.Identity.Name);
            if (!isValid)
            {

               
                Users u = new Users()
                {
                    Password = HashSHA1(m.Password),
                    Email = m.Email,
                    MobileNo = m.MobileNo,
                    Name = m.Name,
                };
                using (var context = new ProductsdbEntities())
                {
                    context.Users.Add(u);
                    context.SaveChanges();
                    Log.Info("New User Registered");
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Email already exists!!");//Adding model error
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
            var model = db.Users.Where(e => e.Email == User.Identity.Name).ToList();
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
                Log.Warn("Attempted unauthorized request");
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (u == null)
            {
                Log.Info("Http not found request");
                return HttpNotFound();
            }
            UserRegistration m = new UserRegistration()
            {
                Id = u.Id,
                Password = u.Password,
                Email = u.Email,
                MobileNo = u.MobileNo,
                Name = u.Name
            };
            return View(m);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, UserRegistration m)
        {
            ModelState["Password"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                Log.Warn("Model Invalid");
                return View(m);
            }

            bool isValid = db.Users.Any(x => x.Email == m.Email && x.Email != User.Identity.Name);
            if (!isValid)
            {
                var u = db.Users.Find(id);
                u.Email = m.Email;
                u.MobileNo = m.MobileNo;
                u.Name = m.Name;
                if (id == m.Id)
                {
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Logout");
            }
            else
            {
                ModelState.AddModelError("Email", "Email already exists!!");
                return View(m);
            }
        }
        [Authorize]
        public ActionResult ChangePassword(int? id)
        {
            if (id == null)
            {
                Log.Debug("Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Users.Where(e => e.Email == User.Identity.Name).ToList();
            Users u = model[0];
            if (u.Id != id)
            {
                Log.Warn("Attempted unauthorized request");
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (u == null)
            {
                Log.Info("Http not Found Request");
                return HttpNotFound();
            }
            return View(new ChangePassword());
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(int? id, ChangePassword c)
        {
            if (c == null)
            {
                return HttpNotFound();
            }
            string arrived = HashSHA1(c.currentPassword);
            bool isValid = db.Users.Any(x => x.Email == User.Identity.Name && x.Password == arrived);
            if (isValid)
            {
                var model = db.Users.Where(e => e.Email == User.Identity.Name).ToList();
                Users u = model[0];
                if (u.Id != id)
                {
                    Log.Warn("Attempted unauthorized request");
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