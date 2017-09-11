using System.Data.Entity;
using BookingHotels.Domain.Entities;
using BookingHotels.DAL.Configurations;
using BookingHotels.DAL.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using BookingHotels.Domain.Identity;

namespace BookingHotels.DAL.EF
{
    public class MyDbContext : IdentityDbContext<ApplicationUser,CustomRole,Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
 
        public MyDbContext() : base("DefaultConnection")
        {
            // Configuration.ProxyCreationEnabled = false;
            // Configuration.LazyLoadingEnabled = false;
        }
        public MyDbContext(string connectionString) : base(connectionString) { }
        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(new MyContextInitializer());
        }
        public sealed class MyContextInitializer : MigrateDatabaseToLatestVersion<MyDbContext, MyDbContextConfiguration> { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            // Booking Configuarations
            modelBuilder.Configurations.Add(new BookingConfig());

            // User Configuarations
            modelBuilder.Configurations.Add(new ApplicationUserConfig());

            // Role Configuarations
            modelBuilder.Configurations.Add(new CustomRoleConfig());
        }
    }
}

