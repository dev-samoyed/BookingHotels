using System;
using BookingHotels.Domain.Entities;

namespace BookingHotels.BLL.DTO
{
    public class FeedbackDTO
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string FeedbackText { get; set; }
        public Hotel Hotel { get; set; }
    }
}
