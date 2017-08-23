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
        // IUnitOfWork object communicates with DAL 
        private IUnitOfWork _unitOfWork { get; set; }
        // Use DI to pass implementation of IUnitOfWork
        public RoomService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        // Get all rooms
        public IEnumerable<RoomDTO> GetRooms()
        {
            var rooms = _unitOfWork.Rooms.GetAll().ToList();
            return Mapper.Map<List<Room>, List<RoomDTO>>(rooms);
        }
        // Get room by it's Id
        public RoomDTO GetRoomById(Guid Id)
        {
            var room = _unitOfWork.Rooms.Get(Id);
            return Mapper.Map<Room, RoomDTO>(room);
        }
        // Add new room
        public void AddRoom(RoomDTO roomDto)
        {
            Room room = Mapper.Map<RoomDTO, Room>(roomDto);
            _unitOfWork.Rooms.Create(room);
            _unitOfWork.Save();
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
