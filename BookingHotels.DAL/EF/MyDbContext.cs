using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BookingHotels.Domain.Entities;
using BookingHotels.DAL.Configurations;
using System.Data.Entity.Migrations;
using BookingHotels.DAL.Migrations;

namespace BookingHotels.DAL.EF
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("MyDB")
        { }
        public MyDbContext(string connectionString) : base(connectionString)
        { }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(new MyContextInitializer());
        }

        public sealed class MyContextInitializer : MigrateDatabaseToLatestVersion<MyDbContext, MyDbContextConfiguration>
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Hotel Config
            modelBuilder.Configurations.Add(new HotelConfig<Hotel>());
        }
    }
    


    

}

