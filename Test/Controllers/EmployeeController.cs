using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Models;

namespace Test.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        EmployeeContext db = new EmployeeContext();
        // GET: Employee
        EmployeeContext _credentials = new EmployeeContext();
        
        public ActionResult Index()
        {
            return View();
        }

    }
}