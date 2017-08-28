using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookingHotels.Web.Models
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public Guid HotelId { get; set; }
        [DataType(DataType.MultilineText)]
        public string FeedbackText { get; set; }
        public FeedbackStars FeedbackStars { get; set; }
        // Links to user and hotel, which had a feedback
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}