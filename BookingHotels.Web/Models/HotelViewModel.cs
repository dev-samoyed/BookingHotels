using BookingHotels.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingHotels.Web.Models
{
    public class HotelViewModel
    {
        public Guid ID { get; set; }
        public string HotelName { get; set; }
        public HotelStars HotelStars { get; set; }
    }
}