using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
