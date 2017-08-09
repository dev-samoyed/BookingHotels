using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingHotels.Web.Models
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FeedbackText { get; set; }
    }
}