using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IBookingService
    {
        // Get all bookings
        IEnumerable<BookingDTO> GetBookings();
        // Get all bokings for particular room
        IEnumerable<BookingDTO> GetBookingsByRoomId(Guid Id);
        // Retuns list of boolean and 2 occupied dates
        List<object> IsRoomOccupied(Guid id, DateTime startDate, DateTime endDate);
        void CreateBooking(BookingDTO bookingDto);
        void Dispose();
    }
}

