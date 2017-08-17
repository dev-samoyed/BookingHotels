using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDTO> GetBookingsByRoom(Guid Id);
        IEnumerable<BookingDTO> GetBookings();
        void CreateBooking(BookingDTO bookingDto);
        void Dispose();
    }
}

