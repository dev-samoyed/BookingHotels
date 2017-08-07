using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.BLL.Interfaces
{
    public interface IRoomService
    {
        // Get 1 room
        RoomDTO GetRoom(Guid ID);
        // Get all rooms
        IEnumerable<RoomDTO> GetRooms();
        // Get rooms in specific hotel
        IEnumerable<RoomDTO> GetRooms(Guid ID);
        void Dispose();
    }
}
