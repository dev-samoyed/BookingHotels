using System.Data.Entity.ModelConfiguration;
using BookingHotels.Domain.Entities;

namespace BookingHotels.DAL.Configurations
{
    //public class UserConfig : EntityTypeConfiguration<ClientProfile>
    public class UserConfig : EntityTypeConfiguration<ApplicationUser>
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
