using System;

namespace BookingHotels.DAL.Entities
{
    public class Feedback
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string FeedbackText { get; set; }
        public Hotel Hotel { get; set; }
    }
}
