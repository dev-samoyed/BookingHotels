using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.Domain.Enums;
using BookingHotels.Domain.Entities;

namespace BookingHotels.BLL.DTO
{
    public class RoomDTO
    {
        public int ID { get; set; }
        public Guid HotelID { get; set; }
        public int RoomNumber { get; set; }
        public RoomType RoomType { get; set; }

        // Link to the hotel
        public Hotel Hotel { get; set; }
    }
}
