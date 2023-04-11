namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class daad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        AdminUserName = c.String(),
                        AdminPassword = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Guid(nullable: false),
                        EmployeeName = c.String(nullable: false),
                        EmployeeEmail = c.String(nullable: false),
                        Password = c.String(),
                        GmailConfirm = c.String(),
                        Leave = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        IsEmailConfirm = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Employees");
            DropTable("dbo.Admins");
        }
    }
}
