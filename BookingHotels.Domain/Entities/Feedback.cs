using System;

namespace BookingHotels.Domain.Entities
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FeedbackText { get; set; }
        // Links to user and hotel, which had a feedback
        public ApplicationUser applicationUser { get; set; }
        public Hotel Hotel { get; set; }
    }
}
