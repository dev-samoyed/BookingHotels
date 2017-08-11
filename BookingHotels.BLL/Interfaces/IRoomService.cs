using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IRoomService
    {
        // Get 1 room
        RoomDTO GetRoom(Guid? Id);
        void AddRoom(RoomDTO roomDto);
        // Get all rooms
        IEnumerable<RoomDTO> GetRooms();
        // Get rooms in specific hotel
        IEnumerable<RoomDTO> GetRooms(Guid Id);
        void DeleteRoom(RoomDTO roomDto);
        void Dispose();
    }
}
