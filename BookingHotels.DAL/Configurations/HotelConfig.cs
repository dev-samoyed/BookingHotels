using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.DAL.Entities;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity;

namespace BookingHotels.DAL.Configurations
{
    public class HotelConfig<T> : EntityTypeConfiguration<T> where T : Hotel
    {
        // Configure models with FLUENT API
        public HotelConfig()
        {
            // Set primary key
            // HasKey(t => t.ID);

            // Specify table
            // ToTable("Hotels");

            // Specify constraints
            HasMany(p => p.Rooms);
            //.WithRequired(p => (Hotel)p.Hotel);
        }
    }
}
