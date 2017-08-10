using System;

namespace BookingHotels.Domain.Entities
{
    public class Booking
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
