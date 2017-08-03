using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingHotels.Web.Models
{
    public class HotelViewModel
    {
        public int ID { get; set; }
        public string HotelName { get; set; }
        //public HotelStars HotelStars { get; set; }
        // Link to rooms collection
        //public virtual List<Room> Rooms { get; set; }
    }
}