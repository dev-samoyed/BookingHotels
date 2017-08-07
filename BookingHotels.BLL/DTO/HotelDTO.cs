using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.DAL.Enums;
using BookingHotels.DAL.Entities;

namespace BookingHotels.BLL.DTO
{
    public class HotelDTO
    {
        public Guid ID { get; set; }
        public string HotelName { get; set; }
        public HotelStars HotelStars { get; set; }
        // Link to rooms collection
        public virtual List<Room> Rooms { get; set; }
    }
}
