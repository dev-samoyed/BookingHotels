using System;
using BookingHotels.Domain.Enums;

namespace BookingHotels.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public int RoomPrice { get; set; }
        public RoomType RoomType { get; set; }
        // Link to the hotel where room is
        public virtual Hotel Hotel { get; set; }

    }
}


