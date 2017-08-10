namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BookingStartDate = c.DateTime(nullable: false),
                        BookingEndDate = c.DateTime(nullable: false),
                        applicationUser_Id = c.Guid(),
                        room_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.applicationUser_Id)
                .ForeignKey("dbo.Room", t => t.room_Id)
                .Index(t => t.applicationUser_Id)
                .Index(t => t.room_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.ApplicationUser", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationUser", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HotelId = c.Guid(nullable: false),
                        Price = c.Int(nullable: false),
                        RoomType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotel", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.Hotel",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HotelName = c.String(),
                        HotelStars = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        FeedbackText = c.String(),
                        applicationUser_Id = c.Guid(),
                        Hotel_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.applicationUser_Id)
                .ForeignKey("dbo.Hotel", t => t.Hotel_Id)
                .Index(t => t.applicationUser_Id)
                .Index(t => t.Hotel_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Feedback", "Hotel_Id", "dbo.Hotel");
            DropForeignKey("dbo.Feedback", "applicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Booking", "room_Id", "dbo.Room");
            DropForeignKey("dbo.Room", "HotelId", "dbo.Hotel");
            DropForeignKey("dbo.Booking", "applicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.AspNetUserLogins", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.AspNetUserClaims", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Feedback", new[] { "Hotel_Id" });
            DropIndex("dbo.Feedback", new[] { "applicationUser_Id" });
            DropIndex("dbo.Room", new[] { "HotelId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Booking", new[] { "room_Id" });
            DropIndex("dbo.Booking", new[] { "applicationUser_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Feedback");
            DropTable("dbo.Hotel");
            DropTable("dbo.Room");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Booking");
        }
    }
}
