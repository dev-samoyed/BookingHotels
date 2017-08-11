using BookingHotels.Domain.Entities;
using System;

namespace BookingHotels.Web.Models
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        // Links to user and room he has booked
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Room Room { get; set; }
    }
}