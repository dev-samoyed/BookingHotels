using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IRoomService
    {
        // Get all rooms
        IEnumerable<RoomDTO> GetRooms();
        // Get room by it's Id
        RoomDTO GetRoomById(Guid Id);
        void AddRoom(RoomDTO roomDto);
        void DeleteRoom(RoomDTO roomDto);
        void Dispose();
    }
}
