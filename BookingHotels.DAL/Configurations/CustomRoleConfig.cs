using BookingHotels.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
