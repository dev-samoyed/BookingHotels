using System;
using System.Collections.Generic;
using System.Linq;
using BookingHotels.BLL.Interfaces;
using BookingHotels.BLL.DTO;
using BookingHotels.Domain.Interfaces;
using AutoMapper;
using BookingHotels.DAL.Entities;

namespace BookingHotels.BLL.Services
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        public RoomService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        // Gets room by ID
        public RoomDTO GetRoom(Guid ID)
        {
            var room = _unitOfWork.Rooms.Get(ID);
            return Mapper.Map<Room, RoomDTO>(room);
        }
        // Get rooms
        public IEnumerable<RoomDTO> GetRooms()
        {
            var rooms = _unitOfWork.Rooms.GetAll().ToList();
            return Mapper.Map<List<Room>, List<RoomDTO>>(rooms);
        }
        // Get rooms in specific hotel
        public IEnumerable<RoomDTO> GetRooms(Guid ID)
        {
            var rooms = _unitOfWork.Rooms.GetAll().ToList();
            return Mapper.Map<List<Room>, List<RoomDTO>>(rooms);
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
