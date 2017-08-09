namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookingHotels.Domain.Entities;
    using BookingHotels.DAL.Enums;
    using EF;

    public sealed class MyDbContextConfiguration : DbMigrationsConfiguration<BookingHotels.DAL.EF.MyDbContext>
    {
        public MyDbContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyDbContext db)
        {
            // Seed 2 hotels
            Hotel hotel1 = new Hotel {
                Id = new Guid("2db76e87-a92d-4c43-a3d5-e0671d8fc894"),
                HotelName = "Hotel #1",
                HotelStars = HotelStars.FiveStarHotel
            };
            Hotel hotel2 = new Hotel
            {
                Id = new Guid("2db76e87-a92d-4c43-a3d5-e0671d8fc895"),
                HotelName = "Hotel #2",
                HotelStars = HotelStars.FourStarHotel
            };
            // Seed 2 rooms to the 1st hotel
            Room room1 = new Room
            {
                Id = new Guid("1d759e1a-b865-4b57-8845-2c7a4cb08e80"),
                HotelId = hotel1.Id,
                RoomNumber = 201,
                RoomType = RoomType.Studio
            };
            Room room2 = new Room
            {
                Id = new Guid("1d759e1a-b865-4b57-8845-2c7a4cb08e81"),
                HotelId = hotel1.Id,
                RoomNumber = 202,
                RoomType = RoomType.DeluxeRoom
            };
            // Seed 1 room to the 2nd hotel
            Room room3 = new Room
            {
                Id = new Guid("1d759e1a-b865-4b57-8845-2c7a4cb08e82"),
                HotelId = hotel2.Id,
                RoomNumber = 203,
                RoomType = RoomType.DeluxeRoom
            };
            
            db.Hotels.AddOrUpdate(hotel1);
            db.Hotels.AddOrUpdate(hotel2);
            db.Rooms.AddOrUpdate(room1);
            db.Rooms.AddOrUpdate(room2);
            db.Rooms.AddOrUpdate(room3);

            db.SaveChanges();

        }

    }
}
