using System;
using BookingHotels.BLL.DTO;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.DALInterfaces;
using BookingHotels.BLL.Infrastructure;
using BookingHotels.BLL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
 
namespace BookingHotels.BLL.Services
{
    public class BookingService : IBookingService
    {
        IUnitOfWork Database { get; set; }

        public BookingService(IUnitOfWork uow)
        {
            Database = uow;
        }
        // Получает объект для сохранения с уровня представления и создает по нему объект Booking и сохраняет его в базу данных.
        public void MakeBooking(BookingDTO bookingDto)
        {
            Room room = Database.Rooms.Get(bookingDto.RoomID);

            // Validatioin
            if (room == null)
                throw new ValidationException("Room was not found", "");
            Booking booking = new Booking
            {
                RoomID = room.ID,
                BookingEndDate = DateTime.Now,
                BookingStartDate = DateTime.Now
            };
            Database.Bookings.Create(booking);
            Database.Save();
        }
        // Получает все комнаты и с помощью автомаппера, преобразует их и передает на уровень представления
        public IEnumerable<RoomDTO> GetRooms()
        {
            // Using automapper for projection of one collection to another
            Mapper.Initialize(cfg => cfg.CreateMap<Room, RoomDTO>());
            return Mapper.Map<IEnumerable<Room>, List<RoomDTO>>(Database.Rooms.GetAll());
        }
        // Передает отдельную комнату на уровень представления.
        public RoomDTO GetRoom(int? id)
        {
            if (id == null)
                throw new ValidationException("Room ID was not set", "");
            var room = Database.Rooms.Get(id.Value);
            if (room == null)
                throw new ValidationException("Room was not found", "");
            // Using automapper for projection of Room to RoomDTO
            Mapper.Initialize(cfg => cfg.CreateMap<Room, RoomDTO>());
            return Mapper.Map<Room, RoomDTO>(room);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<RoomDTO> GetRoom()
        {
            throw new NotImplementedException();
        }
    }
}