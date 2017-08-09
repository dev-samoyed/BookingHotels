using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.DAL.Entities;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Repositories;

namespace BookingHotels.DAL.Configurations
{
    //public class UserConfig : EntityTypeConfiguration<ClientProfile>
    public class UserConfig<T> : EntityTypeConfiguration<T> where T : ClientProfile
    {
        // Configure models with FLUENT API
        public UserConfig()
        {
            // Set primary key
            // HasKey(t => t.ID);
            // Set Foreign key 1-1
            //HasRequired(c => c.ApplicationUser)
            //.WithRequiredPrincipal(c => (ClientProfile)c.ClientProfile);
            

            //.HasForeignKey(c => c.ID);            
            // Specify table
            //ToTable("ClientProfiles");

            // Specify constraints
            // HasMany(p => p.Rooms);
            //.WithRequired(p => (Hotel)p.Hotel);
        }
    }
}
