using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.DAL;
using BookingHotels.DAL.Entities;

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
