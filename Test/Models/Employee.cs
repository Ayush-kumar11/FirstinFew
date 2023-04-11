using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Employee
    {
        [Key]
        public Guid EmployeeID { get; set; } = Guid.NewGuid();
        [Required]
        [Display(Name = "Name")]
        public string EmployeeName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email address")]
        public string EmployeeEmail { get; set; }
        public string Password { get; set; }
        public string GmailConfirm { get; set; }
        public string Leave { get; set; }

    }
}