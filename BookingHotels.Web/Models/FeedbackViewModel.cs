using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingHotels.Web.Models
{
    public class FeedbackViewModel
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string FeedbackText { get; set; }
    }
}