using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.Domain.Entities
{
    public class Booking
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}
