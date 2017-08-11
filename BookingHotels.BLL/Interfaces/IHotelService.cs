using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IHotelService
    {
        // Get 1 hotel by ID
        HotelDTO GetHotel(Guid? id);
        IEnumerable<HotelDTO> GetHotels();
        void AddHotel(HotelDTO hotel);
        void DeleteHotel(HotelDTO hotel);
        void Dispose();
        
    }
}
