using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.Domain;
using BookingHotels.Domain.Entities;

namespace BookingHotels.BLL.DTO
{
    public class FeedbackDTO
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string FeedbackText { get; set; }
        public Hotel Hotel { get; set; }
    }
}
