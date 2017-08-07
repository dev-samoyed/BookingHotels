using BookingHotels.Domain.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BookingHotels.DAL.EF
{
    public class MyIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyIdentityDbContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }

}
