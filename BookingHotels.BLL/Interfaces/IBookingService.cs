using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;
namespace BookingHotels.BLL.Interfaces
{
    public interface IBookingService
    {
        void AddBooking(BookingDTO bookingDto);
        void Dispose();
    }
}

