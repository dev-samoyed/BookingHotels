using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.DAL.Entities
{
    public class Booking
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoomID { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}
