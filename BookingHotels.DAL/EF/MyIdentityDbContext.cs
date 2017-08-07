using BookingHotels.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.DAL.EF
{
    public class MyIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyIdentityDbContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }

}
