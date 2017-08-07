using System;
using BookingHotels.BLL.DTO;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.DALInterfaces;
using BookingHotels.BLL.Infrastructure;
using BookingHotels.BLL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;

namespace BookingHotels.BLL.Services
{
    public class BookingService : IBookingService
    {
        private  IUnitOfWork _unitOfWork { get; set; }
        // BookingService в конструкторе принимает объект IUnitOfWork
        // через который идет взаимодействие с уровнем DAL.
        public BookingService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        // Мы не задаем в конструкторе явно объект IUnitOfWork, используем внедрение зависимостей
        // для передачи конкретной реализации данного интерфейса в BookingService (ninject)

        // Получает объект бронирования для сохранения с уровня представления
        // и создает по нему объект Booking и сохраняет его в базу данных.
        public void MakeBooking(BookingDTO bookingDto)
        {
            Room room = _unitOfWork.Rooms.Get(bookingDto.RoomID);

            // Validatioin
            if (room == null)
                throw new ValidationException("Room was not found", "");
            Booking booking = new Booking
            {
                RoomID = room.ID,
                BookingEndDate = DateTime.Now,
                BookingStartDate = DateTime.Now
            };
            _unitOfWork.Bookings.Create(booking);
            _unitOfWork.Save();
        }

        // Получает все комнаты и с помощью автомаппера
        // преобразует их и передает на уровень представления
        public IEnumerable<RoomDTO> GetRooms()
        {
            // Using automapper for projection of one collection to another

           // Mapper.Initialize(cfg => cfg.CreateMap<Room, RoomDTO>());

            var rooms = _unitOfWork.Rooms.GetAll().ToList();

            return Mapper.Map<List<Room>, List<RoomDTO>>(rooms);
        }

        // Передает отдельную комнату на уровень представления.
        public RoomDTO GetRoom(Guid? id)
        {
            if (id == null)
                throw new ValidationException("Room ID was not set", "");
            var room = _unitOfWork.Rooms.Get(id.Value);
            if (room == null)
                throw new ValidationException("Room was not found", "");
            // Using automapper for projection of Room to RoomDTO usind data of room object
            //Mapper.Initialize(cfg => cfg.CreateMap<Room, RoomDTO>());
            return Mapper.Map<Room, RoomDTO>(room);
        }
        public RoomDTO GetRoomsIn(Guid? id)
        {
            if (id == null)
                throw new ValidationException("Room ID was not set", "");
            var room = _unitOfWork.Rooms.Get(id.Value);
            if (room == null)
                throw new ValidationException("Room was not found", "");
            // Using automapper for projection of Room to RoomDTO usind data of room object
            //Mapper.Initialize(cfg => cfg.CreateMap<Room, RoomDTO>());
            return Mapper.Map<Room, RoomDTO>(room);
        }

        // Gets HotelDTO by ID and sends it to view
        public HotelDTO GetHotel(Guid id)
        {
            Hotel hotel = _unitOfWork.Hotels.Get(id);

            return Mapper.Map<Hotel, HotelDTO>(hotel);

        }

        public IEnumerable<HotelDTO> GetHotels()
        {
            var hotels = _unitOfWork.Hotels.GetAll().ToList();
            return Mapper.Map<List<Hotel>, List<HotelDTO>>(hotels);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<RoomDTO> GetRoom()
        {
            throw new NotImplementedException();
        }
    }
}