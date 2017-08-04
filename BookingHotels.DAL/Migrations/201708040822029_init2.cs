namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        RoomID = c.Guid(nullable: false),
                        BookingStartDate = c.DateTime(nullable: false),
                        BookingEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        FeedbackText = c.String(),
                        Hotel_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.Hotel_ID)
                .Index(t => t.Hotel_ID);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        HotelName = c.String(),
                        HotelStars = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        HotelID = c.Guid(nullable: false),
                        RoomNumber = c.Int(nullable: false),
                        RoomType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .Index(t => t.HotelID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "Hotel_ID", "dbo.Hotels");
            DropForeignKey("dbo.Rooms", "HotelID", "dbo.Hotels");
            DropIndex("dbo.Rooms", new[] { "HotelID" });
            DropIndex("dbo.Feedbacks", new[] { "Hotel_ID" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Hotels");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Bookings");
        }
    }
}
