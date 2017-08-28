using System;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Enums;

namespace BookingHotels.BLL.DTO
{
    public class FeedbackDTO
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public Guid HotelId { get; set; }
        public string FeedbackText { get; set; }
        public FeedbackStars FeedbackStars { get; set; }
        // Links to user and hotel, which had a feedback
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
