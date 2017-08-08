using BookingHotels.DAL.Configurations;
using BookingHotels.DAL.Entities;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BookingHotels.DAL.EF
{
    public class MyIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyIdentityDbContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Configuarations via Fluent api
            modelBuilder.Configurations.Add(new UserConfig<ClientProfile>());
        }
    }

}
