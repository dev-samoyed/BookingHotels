namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookingHotels.DAL.Entities;
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
            // Seed hotels
            Hotel hotel1 = new Hotel {
                ID = new Guid("2db76e87-a92d-4c43-a3d5-e0671d8fc894"),
                HotelName = "Hotel #1",
                HotelStars = HotelStars.FiveStarHotel
            };
            Hotel hotel2 = new Hotel
            {
                ID = new Guid("2db76e87-a92d-4c43-a3d5-e0671d8fc895"),
                HotelName = "Hotel #2",
                HotelStars = HotelStars.FourStarHotel
            };
            // Seed rooms to 1st hotel
            Room room1 = new Room
            {
                ID = new Guid("1d759e1a-b865-4b57-8845-2c7a4cb08e80"),
                HotelID = hotel1.ID,
                RoomNumber = 201,
                RoomType = RoomType.Studio
            };
            Room room2 = new Room
            {
                ID = new Guid("1d759e1a-b865-4b57-8845-2c7a4cb08e81"),
                HotelID = hotel1.ID,
                RoomNumber = 202,
                RoomType = RoomType.DeluxeRoom
            };
            // Seed room to 2nd hotel
            Room room3 = new Room
            {
                ID = new Guid("1d759e1a-b865-4b57-8845-2c7a4cb08e82"),
                HotelID = hotel2.ID,
                RoomNumber = 203,
                RoomType = RoomType.DeluxeRoom
            };

            //Hotel hotel2 = new Hotel { ID = Guid.NewGuid(), HotelName = "Hotel 2", HotelStars = HotelStars.OneStarHotel };
            //Room room2 = new Room { ID = Guid.NewGuid(), RoomNumber = 3, RoomType = RoomType.Studio, Hotel = hotel1 };

            db.Hotels.AddOrUpdate(hotel1);
            db.Hotels.AddOrUpdate(hotel2);
            db.Rooms.AddOrUpdate(room1);
            db.Rooms.AddOrUpdate(room2);
            db.Rooms.AddOrUpdate(room3);

            db.SaveChanges();

        }

    }
}
