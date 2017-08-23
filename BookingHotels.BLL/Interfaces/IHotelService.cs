using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IHotelService
    {
        // Get All hotels
        IEnumerable<HotelDTO> GetHotels();
        // Get hotel by it's Id
        HotelDTO GetHotelById(Guid id);
        void AddHotel(HotelDTO hotel);
        void DeleteHotel(HotelDTO hotel);
        void Dispose();
    }
}
