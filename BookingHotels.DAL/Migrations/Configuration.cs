namespace BookingHotels.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookingHotels.Domain.Entities;
    using BookingHotels.Domain.Enums;
    using EF;

    public sealed class MyDbContextConfiguration : DbMigrationsConfiguration<BookingHotels.DAL.EF.MyDbContext>
    {
        public MyDbContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyDbContext db)
        {
            //Hotel hotel1 = new Hotel { HotelName = "Hotel 1", HotelStars = HotelStars.OneStarHotel };
            //Room room1 = new Room { RoomNumber = 1, RoomType = RoomType.DeluxeRoom, Hotel = hotel1 };

            //db.Rooms.Add(room1);
            //db.Rooms.Add(room1);
            //db.SaveChanges();
        }
    }
}
