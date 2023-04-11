namespace Test.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Test.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Test.Models.EmployeeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Test.Models.EmployeeContext context)
        {
            var users = new List<User>
            {
                new User() {UserId = Guid.NewGuid().ToString(), Username = "Admin1" , Password=encryptPassword("Admin100"),Role="Admin",IsEmailConfirm = true}
            };
            users.Add(new User() { UserId = Guid.NewGuid().ToString(), Username = "Admin2", Password = encryptPassword("Admin200"), Role = "Admin", IsEmailConfirm = true });
            users.Add(new User() { UserId = Guid.NewGuid().ToString(), Username = "Employee1", Password = encryptPassword("Employee100"), Role = "Employee", IsEmailConfirm = true });
            users.Add(new User() { UserId = Guid.NewGuid().ToString(), Username = "Employee2", Password = encryptPassword("Employee200"), Role = "Employee", IsEmailConfirm = true });

            users.ForEach(x => context.Users.Add(x));
            base.Seed(context);
            context.SaveChanges();
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
    }
}
