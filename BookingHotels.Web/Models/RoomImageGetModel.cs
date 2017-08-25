using System;
using BookingHotels.Domain.Enums;
using BookingHotels.Domain.Entities;

namespace BookingHotels.BLL.DTO
{
    public class RoomImageGetModel
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
    }
}
