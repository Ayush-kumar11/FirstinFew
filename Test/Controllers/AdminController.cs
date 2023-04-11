using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using Test.Repository;

namespace Test.Controllers
{
    [Authorize(Roles ="Admin,Employee")]
    public class AdminController : Controller
    {
        private IEmployeeRepository<Employee> _employeeRepository = null;
        public AdminController()
        {
            this._employeeRepository = new EmployeeRepository<Employee>();
        }
        public AdminController(IEmployeeRepository<Employee> employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {            
            return View();
        }
        public ActionResult Employee()
        {
            var model = _employeeRepository.GetAll();
            return View(model);
        }
        public ActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployee([Bind(Include = "EmployeeID,EmployeeName,EmployeeEmail,Password,GmailConfirm,Leave")]Employee employee)
        {
            if(ModelState.IsValid)
            {
                employee.EmployeeID = Guid.NewGuid();
                employee.GmailConfirm = "Not Approved";
                employee.Leave = "No Leave";
                _employeeRepository.Add(employee);

                string confirmationUrl = Url.Action("Index","Gmail",new { id = employee.EmployeeID.ToString() },Request.Url.Scheme);
                string body = string.Format("<a href='{0}'>Click here to set password</a>",confirmationUrl);
                SendEmail(body, employee.EmployeeEmail);
                _employeeRepository.Save();
                return RedirectToAction("Employee");
            }
            return View(employee);
        }
        public void SendEmail(string body,string emailAddress)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ayushkumar74884846@gmail.com");
                message.To.Add(emailAddress);
                message.IsBodyHtml = true;
                message.Subject = "Generate Password";
                message.Body = body;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("ayushkumar74884846@gmail.com", "hnaihrrbegdkqzss");
                client.EnableSsl = true;
                client.Send(message);
                Console.WriteLine($"Email sent to {emailAddress}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Sending email to {emailAddress} : {ex.Message}");
            }
        }
        public ActionResult DeleteEmployee(Guid? id)
        {
            Employee model = _employeeRepository.GetById(id);
            return View(model);
        }
        [HttpGet,ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            _employeeRepository.Delete(id);
            _employeeRepository.Save();
            ViewBag.message="Deleted";
            return RedirectToAction("Employee");
        }
        public ActionResult EditEmployee(Guid? id)
        {
            Employee model = _employeeRepository.GetById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditEmployee(Employee model)
        {
            if(ModelState.IsValid){
                _employeeRepository.Update(model);
                _employeeRepository.Save();
                return RedirectToAction("Employee");
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Leave()
        {
            return View();
        }
    }
}