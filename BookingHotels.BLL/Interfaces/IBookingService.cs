using BookingHotels.BLL.DTO;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDTO> GetBookings();
        void CreateBooking(BookingDTO bookingDto);
        void Dispose();
    }
}

