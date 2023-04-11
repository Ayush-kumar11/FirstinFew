using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        public bool IsEmailConfirm { get; set; }

    }
}