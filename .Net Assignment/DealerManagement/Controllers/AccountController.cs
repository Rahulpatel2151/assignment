using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DealerManagement.Models;
using log4net;
using DealerManagement.BAL;

namespace DealerManagement.Controllers
{
    public class AccountController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccountController));
        // GET: Users
        IUserManager _userManager;
        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            string username = User.Identity.Name;

            try
            {
                UserView u = _userManager.findUser(username);
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

            try
            {
                string hashedPassword = HashSHA1(m.Password);//hashing
                bool isValid = _userManager.isAuthenticated(m.Email, hashedPassword);
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
            bool isValid = _userManager.isExisting(m, User.Identity.Name);
            if (!isValid)
            {
                m.Password = HashSHA1(m.Password);
                _userManager.addUser(m);
                Log.Info("New User Registered");
                return RedirectToAction("Login", "Account");
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
            UserView u;
            try
            {
                u = _userManager.findUser(User.Identity.Name);
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
                Phone = u.Phone,
                Name = u.Name,
                Address=u.Address,
                PinCode=u.PinCode,
                City=u.City
            };
            return View(m);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, UserRegistration m)
        {
            ModelState["Password"].Errors.Clear();
            ModelState["ConfirmPassword"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                Log.Warn("Model Invalid");
                return View(m);
            }
            UserView u;
            try
            {
                u = _userManager.findUser(User.Identity.Name);
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
            bool isValid = _userManager.isExisting(m, User.Identity.Name);
            if (!isValid)
            {
                _userManager.editUser(id, m);

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
            UserView u = _userManager.findUser(User.Identity.Name);
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
            if (c == null || id == null)
            {
                return HttpNotFound();
            }
            UserView u = _userManager.findUser(User.Identity.Name);
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
            string arrived = HashSHA1(c.currentPassword);
            bool isValid = _userManager.isAuthenticated(User.Identity.Name, arrived);
            if (isValid)
            {
                _userManager.editPassword((int)id, HashSHA1(c.NewPassword));

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