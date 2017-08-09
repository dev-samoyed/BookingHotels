using System;

namespace BookingHotels.Domain.Entities
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FeedbackText { get; set; }
        public Hotel Hotel { get; set; }
    }
}
