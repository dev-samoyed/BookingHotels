using BookingHotels.Domain.Entities;
using System;

namespace BookingHotels.Web.Models
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FeedbackText { get; set; }
        public Hotel Hotel { get; set; }
    }
}