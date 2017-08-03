namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                        BookingStartDate = c.DateTime(nullable: false),
                        BookingEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        FeedbackText = c.String(),
                        Hotel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.Hotel_ID)
                .Index(t => t.Hotel_ID);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HotelName = c.String(),
                        HotelStars = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HotelID = c.Guid(nullable: false),
                        RoomNumber = c.Int(nullable: false),
                        RoomType = c.Int(nullable: false),
                        Hotel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.Hotel_ID)
                .Index(t => t.Hotel_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "Hotel_ID", "dbo.Hotels");
            DropForeignKey("dbo.Rooms", "Hotel_ID", "dbo.Hotels");
            DropIndex("dbo.Rooms", new[] { "Hotel_ID" });
            DropIndex("dbo.Feedbacks", new[] { "Hotel_ID" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Hotels");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Bookings");
        }
    }
}
