namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users_and_roles_altering_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.Users");
            DropPrimaryKey("dbo.Users");
            AddColumn("dbo.AspNetUserRoles", "RoleGuidId", c => c.Guid());
            AddColumn("dbo.AspNetUserRoles", "UserGuidId", c => c.Guid());
            AlterColumn("dbo.Users", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.Users");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUserRoles", "UserGuidId");
            DropColumn("dbo.AspNetUserRoles", "RoleGuidId");
            AddPrimaryKey("dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.Users", "Id");
        }
    }
}
