using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.Domain.Identity;
using System.Data.Entity.ModelConfiguration;
using BookingHotels.Domain.Entities;

namespace BookingHotels.DAL.Configurations
{
    public class BookingConfig : EntityTypeConfiguration<Booking>
    {
        // Configure with FLUENT API
        public BookingConfig()
        {
            HasRequired(t => t.ApplicationUser)
            .WithMany()
            .HasForeignKey(t => t.ApplicationUserId);

            HasRequired(t => t.Room)
            .WithMany()
            .HasForeignKey(t => t.RoomId);            
        }
    }

}
