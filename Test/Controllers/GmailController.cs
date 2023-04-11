using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class GmailController : Controller
    {
        EmployeeContext db = new EmployeeContext();
        public ActionResult Index(Guid? id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Guid id,EmployeePassword p)
        {
            if(ModelState.IsValid)
            {
                Employee employee = db.Employees.Find(id);
                if(employee == null)
                {
                    return HttpNotFound();
                }

                using(var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(p.ConfirmPassword));
                    var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-","").ToLower();

                    employee.Password = hashedPassword;
                }
                employee.GmailConfirm = "Approved";
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Login","Account");
        }
    }
}