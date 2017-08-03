using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BookingHotels.Domain.Entities;
using BookingHotels.DAL.Configurations;

namespace BookingHotels.DAL.EF
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("MyDB")
        { }
        public MyDbContext(string connectionString)
            : base(connectionString)
        { }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotel>().ToTable("Hotels");
            modelBuilder.Configurations.Add(new HotelConfig<Hotel>());
        }
    }

}

