using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingHotels.BLL.DTO;

namespace BookingHotels.Web.Models
{
    public class RoomViewModel
    {
        public int ID { get; set; }
        public Guid HotelID { get; set; }
        public int RoomNumber { get; set; }
        //public RoomType RoomType { get; set; }

        // Link to the hotel
        //public Hotel Hotel { get; set; }
    }
}