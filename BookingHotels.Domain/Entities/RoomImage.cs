using System;

namespace BookingHotels.Domain.Entities
{
    public class RoomImage
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        // Link to the room pictured on this picture
        public virtual Room Room { get; set; }
    }
}
