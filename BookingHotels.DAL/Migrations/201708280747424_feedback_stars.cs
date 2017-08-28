namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedback_stars : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Feedbacks", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Feedbacks", new[] { "ApplicationUser_Id" });
            RenameColumn(table: "dbo.Feedbacks", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AddColumn("dbo.Feedbacks", "FeedbackStars", c => c.Int(nullable: false));
            AlterColumn("dbo.Feedbacks", "ApplicationUserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Feedbacks", "ApplicationUserId");
            AddForeignKey("dbo.Feedbacks", "ApplicationUserId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Feedbacks", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "UserId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Feedbacks", "ApplicationUserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Feedbacks", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Feedbacks", "ApplicationUserId", c => c.Guid());
            DropColumn("dbo.Feedbacks", "FeedbackStars");
            RenameColumn(table: "dbo.Feedbacks", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            CreateIndex("dbo.Feedbacks", "ApplicationUser_Id");
            AddForeignKey("dbo.Feedbacks", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
        }
    }
}
