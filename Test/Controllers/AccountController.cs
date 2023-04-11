using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Models;

namespace Test.Controllers
{
    public class AccountController : Controller
    {
        EmployeeContext _credentials = new EmployeeContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin model)
        {
            User user = _credentials.Users.FirstOrDefault(x => x.Username == model.AdminUserName);
            var encrypt_password = encryptPassword(model.AdminPassword);
            if(user.Password == encrypt_password)
            {
                if(user.Role == "Admin")
                {
                    FormsAuthentication.SetAuthCookie(model.AdminUserName, false);                   
                    return RedirectToAction("Index", "Admin");
                }
                else if(user.Role == "Employee")
                {
                    FormsAuthentication.SetAuthCookie(model.AdminUserName, false);
                    return RedirectToAction("Index", "Employee");
                }

            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        private string encryptPassword(string pass)
        {
            string password = "";
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return password;
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            if (ModelState.IsValid)
            {
                _credentials.Users.Add(model);
                _credentials.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Account");
        }
    }
}