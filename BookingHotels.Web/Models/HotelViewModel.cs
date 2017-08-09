using BookingHotels.Domain.Entities;
using BookingHotels.DAL.Enums;
using System;
using System.Collections.Generic;

namespace BookingHotels.Web.Models
{
    public class HotelViewModel
    {
        public Guid Id { get; set; }
        public string HotelName { get; set; }
        public HotelStars HotelStars { get; set; }
        public virtual List<Room> Rooms { get; set; }
    }
}