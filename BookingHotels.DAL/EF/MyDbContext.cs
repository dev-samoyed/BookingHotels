using System.Data.Entity;
using BookingHotels.DAL.Entities;
using BookingHotels.DAL.Configurations;
using BookingHotels.DAL.Migrations;

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

