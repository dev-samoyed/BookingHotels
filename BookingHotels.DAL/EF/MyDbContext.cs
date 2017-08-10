using System.Data.Entity;
using BookingHotels.Domain.Entities;
using BookingHotels.DAL.Configurations;
using BookingHotels.DAL.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using BookingHotels.Domain.Identity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BookingHotels.DAL.EF
{
    public class MyDbContext : IdentityDbContext<ApplicationUser,CustomRole,Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

 
        public MyDbContext() : base("DefaultConnection")
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
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            
            // Hotel Configuarations
            modelBuilder.Configurations.Add(new HotelConfig());

            // Users Configuarations
            modelBuilder.Configurations.Add(new ApplicationUserConfig());

            // Roles Configuarations
            modelBuilder.Configurations.Add(new CustomRoleConfig());


            // Roles and Users
            //modelBuilder.Entity<CustomUserRole>().Ignore(t => t.ApplicationUser_Id);

            //modelBuilder.Entity<IdentityRole>().ToTable("Roles").HasKey(k => k.Id);
            //modelBuilder.Entity<IdentityUser>().ToTable("Users").HasKey(k => k.Id);
            // Bridge table
            //modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
            
        }
    }
}

