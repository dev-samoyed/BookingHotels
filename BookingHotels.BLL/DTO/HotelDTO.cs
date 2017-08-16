using System;
using System.Collections.Generic;
using BookingHotels.Domain.Enums;
using BookingHotels.Domain.Entities;

namespace BookingHotels.BLL.DTO
{
    public class HotelDTO
    {
        public Guid Id { get; set; }
        public string HotelName { get; set; }
        public HotelStars HotelStars { get; set; }
        // Link to rooms collection
        public virtual List<Room> Rooms { get; set; }
    }
}
