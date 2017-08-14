using System;
using BookingHotels.DAL.Enums;
using BookingHotels.Domain.Entities;

namespace BookingHotels.Web.Models
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public int RoomPrice { get; set; }
        public RoomType RoomType { get; set; }
        // Link to the hotel
        public virtual Hotel Hotel { get; set; }
    }
}