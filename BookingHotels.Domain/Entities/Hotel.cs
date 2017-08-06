using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotels.Domain.Entities
{
    public class Hotel
    {
        public Guid ID { get; set; }
        public string HotelName { get; set; }
        public HotelStars HotelStars { get; set; }
        // Link to rooms collection
        public virtual List<Room> Rooms { get; set; }

    }
}
