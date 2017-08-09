using System.Data.Entity;
using BookingHotels.DAL.Entities;
using BookingHotels.DAL.Configurations;
using BookingHotels.DAL.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using BookingHotels.Domain.Repositories;

namespace BookingHotels.DAL.EF
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public MyDbContext() : base("DefaultConnection")
        { }
        public MyDbContext(string connectionString) : base(connectionString)
        { }
        static MyDbContext()
        {
  
            Database.SetInitializer<MyDbContext>(new MyContextInitializer());
        }
        public sealed class MyContextInitializer : MigrateDatabaseToLatestVersion<MyDbContext, MyDbContextConfiguration>
        { 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Hotel Configuarations via Fluent api
            modelBuilder.Configurations.Add(new HotelConfig<Hotel>());

            // User Configuarations via Fluent api
            modelBuilder.Configurations.Add(new UserConfig<ClientProfile>());
        }
    }
}

