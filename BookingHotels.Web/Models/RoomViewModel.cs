using System;
using BookingHotels.Domain.Enums;
using BookingHotels.Domain.Entities;
using System.Collections.Generic;
using System.Web;

namespace BookingHotels.Web.Models
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public int RoomPrice { get; set; }
        public IEnumerable<HttpPostedFileBase> Images { get; set; }
        public string RoomType { get; set; }
        // Link to the hotel
        public virtual Hotel Hotel { get; set; }
    }
}