namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RoomPrice", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "RoomPrice");
        }
    }
}
