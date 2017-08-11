using BookingHotels.Domain.Identity;
using System.Data.Entity.ModelConfiguration;

namespace BookingHotels.DAL.Configurations
{
    class CustomRoleConfig : EntityTypeConfiguration<CustomRole>
    {
        // Configure models with FLUENT API
        public CustomRoleConfig()
        {
            HasMany(p => p.Users)
            .WithRequired()
            .HasForeignKey(p => p.RoleId);
        }
    }
}
