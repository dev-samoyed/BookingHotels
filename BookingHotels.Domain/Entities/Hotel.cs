using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.Domain.Enums;

namespace BookingHotels.Domain.Entities
{
    public class Hotel
    {
        public int ID { get; set; }
        public string HotelName { get; set; }
        public HotelStars HotelStars { get; set; }
        // Link to rooms collection
        public virtual List<Room> Rooms { get; set; }

    }
}
