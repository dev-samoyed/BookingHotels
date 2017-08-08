using System;
using BookingHotels.DAL.Enums;

namespace BookingHotels.DAL.Entities
{
    public class Room
    {
        public Guid ID { get; set; }
        public Guid HotelID { get; set; }
        public int RoomNumber { get; set; }
        public RoomType RoomType { get; set; }

        // Link to the hotel
        public Hotel Hotel { get; set; }

    }
}


