using System;
using System.Collections.Generic;
using System.Linq;
using BookingHotels.BLL.Interfaces;
using BookingHotels.BLL.DTO;
using BookingHotels.Domain.Interfaces;
using AutoMapper;
using BookingHotels.Domain.Entities;

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
        public RoomDTO GetRoom(Guid? ID)
        {
            var room = _unitOfWork.Rooms.Get(ID);
            return Mapper.Map<Room, RoomDTO>(room);
        }
        // Add new room
        public void AddRoom(RoomDTO roomDto)
        {
            Room room = Mapper.Map<RoomDTO, Room>(roomDto);

            _unitOfWork.Rooms.Create(room);
            _unitOfWork.Save();
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

        // Delete room
        public void DeleteRoom(RoomDTO roomDto)
        {
            Room room = Mapper.Map<RoomDTO, Room>(roomDto);

            _unitOfWork.Rooms.Delete(room.Id);
            _unitOfWork.
            Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
