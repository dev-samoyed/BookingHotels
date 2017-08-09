using System.Data.Entity;
using BookingHotels.Domain.Entities;
using BookingHotels.DAL.Configurations;
using BookingHotels.DAL.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookingHotels.DAL.EF
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        // public DbSet<ApplicationUser> ApplicationUsers { get; set; }

 
        public MyDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
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
            modelBuilder.Configurations.Add(new HotelConfig());

            // User Configuarations via Fluent api
            modelBuilder.Configurations.Add(new UserConfig());

            // Roles and Users
            modelBuilder.Entity<IdentityRole>().ToTable("Roles").HasKey(k => k.Id);
            modelBuilder.Entity<IdentityUser>().ToTable("Users").HasKey(k => k.Id);
            // Bridge table
            modelBuilder.Entity<ApplicationUserRoles>().ToTable("UserRoles");


            //HasKey("ApplicationUser_Id");
        }
    }
}

