using System;

namespace BookingHotels.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        // Links to user and room her has booked
        public ApplicationUser applicationUser { get; set; }
        public Room room { get; set; }
    }
}
