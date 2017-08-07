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
using Microsoft.AspNet.Identity.EntityFramework;
using BookingHotels.DAL.Repositories;

namespace UserStore.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}


namespace BookingHotels.DAL.EF
{
    public class MyDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public MyDbContext() : base("DefaultConnection")
        { }
        public MyDbContext(string connectionString) : base(connectionString)
        { }
        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(new MyContextInitializer());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Hotel Configuarations via Fluent api
            modelBuilder.Configurations.Add(new HotelConfig<Hotel>());
        }
        public sealed class MyContextInitializer : MigrateDatabaseToLatestVersion<MyDbContext, MyDbContextConfiguration>
        { }


    }
    


    

}

