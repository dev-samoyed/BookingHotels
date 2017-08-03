using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingHotels.Web.Models
{
    public class BookingViewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}