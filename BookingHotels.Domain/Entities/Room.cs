using System;
using BookingHotels.DAL.Enums;

namespace BookingHotels.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public int Price { get; set; }
        public RoomType RoomType { get; set; }
        // Link to the hotel where room is
        public Hotel Hotel { get; set; }

    }
}


