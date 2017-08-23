namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomImage_entity5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomImages", "RoomId", "dbo.Rooms");
            DropIndex("dbo.RoomImages", new[] { "RoomId" });
            DropTable("dbo.RoomImages");
        }
    }
}
