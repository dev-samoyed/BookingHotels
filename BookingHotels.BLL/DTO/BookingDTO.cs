using System;

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
