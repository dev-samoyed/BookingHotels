using System;
using BookingHotels.Domain.Entities;

namespace BookingHotels.BLL.DTO
{
    public class FeedbackDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid HotelId { get; set; }
        public string FeedbackText { get; set; }
        // Links to user and hotel, which had a feedback
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
