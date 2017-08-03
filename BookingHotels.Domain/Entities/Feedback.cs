using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.Domain.Entities
{
    public class Feedback
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string FeedbackText { get; set; }
        public Hotel Hotel { get; set; }
    }
}
