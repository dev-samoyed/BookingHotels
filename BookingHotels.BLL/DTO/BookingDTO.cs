using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.Domain;
using BookingHotels.Domain.Entities;

namespace BookingHotels.BLL.DTO
{
    public class BookingDTO
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}
