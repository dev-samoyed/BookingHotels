using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IHotelService
    {
        // Get 1 hotel by ID
        HotelDTO GetHotel(Guid? id);
        // Get 1 hotel, where room is
        HotelDTO GetHotelByRoom(Guid id);
        IEnumerable<HotelDTO> GetHotels();
        void Dispose();
        
    }
}
