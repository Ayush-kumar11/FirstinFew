using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("MyConnection")
        {
            //Database.SetInitializer<EmployeeContext>(new AdminDbInitializer<EmployeeContext>());
            //Database.SetInitializer(new EmployeeDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set;}
        public DbSet<Admin> Admins { get; set;}        
    }
    
}