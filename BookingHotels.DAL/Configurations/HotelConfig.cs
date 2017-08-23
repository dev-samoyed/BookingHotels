using BookingHotels.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BookingHotels.DAL.Configurations
{
    public class HotelConfig : EntityTypeConfiguration<Hotel>
    {
        // Configure with FLUENT API
        public HotelConfig()
        {
            // Set primary key
            // HasKey(t => t.ID);

            // Specify table
            // ToTable("Hotels");

            // Specify constraints
            //HasMany(p => p.Rooms);
            //.WithRequired(p => (Hotel)p.Hotel);
        }
    }
}
