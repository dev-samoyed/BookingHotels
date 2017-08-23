using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IRoomImageService
    {
        IEnumerable<RoomImageDTO> GetRoomImages();
        IEnumerable<RoomImageDTO> GetRoomImagesByRoomId(Guid Id);
        void Create(RoomImageDTO roomImageDto);
        void Dispose();
    }
}

