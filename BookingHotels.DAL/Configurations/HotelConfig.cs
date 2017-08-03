using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.Domain.Entities;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity;

namespace BookingHotels.DAL.Configurations
{
    public class HotelConfig<T> : EntityTypeConfiguration<T> where T : Hotel
    {
        public HotelConfig()
        {
            //configure model with fluent API

            //HasMany(p => p.Rooms)
            //    .WithRequired(p => (Hotel)p.Hotel);

            //set primary key
            HasKey(t => t.ID);

            
        }
    }
}
