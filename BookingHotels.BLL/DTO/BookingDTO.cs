using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.DAL;
using BookingHotels.DAL.Entities;

namespace BookingHotels.BLL.DTO
{
    public class BookingDTO
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoomID { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}
