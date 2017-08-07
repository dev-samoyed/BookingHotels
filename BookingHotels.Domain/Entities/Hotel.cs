using System;
using System.Collections.Generic;
using BookingHotels.DAL.Enums;

namespace BookingHotels.DAL.Entities
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
