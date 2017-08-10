using System.Data.Entity.ModelConfiguration;
using BookingHotels.Domain.Entities;

namespace BookingHotels.DAL.Configurations
{
    //public class UserConfig : EntityTypeConfiguration<ClientProfile>
    public class ApplicationUserConfig : EntityTypeConfiguration<ApplicationUser>
    {
        // Configure with FLUENT API
        public ApplicationUserConfig()
        {
            HasMany(p => p.Roles)
            .WithRequired()
            .HasForeignKey(p => p.UserId);
        }
    }
}
