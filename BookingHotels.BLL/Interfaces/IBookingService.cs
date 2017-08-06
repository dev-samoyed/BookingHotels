using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;
namespace BookingHotels.BLL.Interfaces
{
    public interface IBookingService
    {
        void MakeBooking(BookingDTO bookingDto);
        RoomDTO GetRoom(Guid? id);
        IEnumerable<RoomDTO> GetRooms();
        void Dispose();
    }
}

