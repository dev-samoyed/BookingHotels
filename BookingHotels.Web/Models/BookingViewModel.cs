using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingHotels.Web.Models
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}