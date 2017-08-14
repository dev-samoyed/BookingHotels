using BookingHotels.BLL.DTO;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Interfaces;
using BookingHotels.BLL.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BookingHotels.BLL.Services
{
    public class BookingService : IBookingService
    {
        // IUnitOfWork object communicates with DAL 
        private IUnitOfWork _unitOfWork { get; set; }

        // Use DI to pass implementation of IUnitOfWork
        public BookingService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        //public IEnumerable<BookingDTO> GetBookingsByRoomId(Guid Id)
        //{
        //    var allBookings = _unitOfWork.Bookings.GetAll().ToList();
        //    var bookings = from b
        //                    in allBookings
        //                   where b.RoomId==Id
        //                   select b;
        //    return Mapper.Map<List<Booking>, List<BookingDTO>>(bookings);
        //}

        public IEnumerable<BookingDTO> GetBookings()
        {
            var bookings = _unitOfWork.Bookings.GetAll().ToList();
            return Mapper.Map<List<Booking>, List<BookingDTO>>(bookings);
        }

        // Get bookingDto from Web, create booking object and save to db
        public void CreateBooking(BookingDTO bookingDto)
        {
             Booking booking = Mapper.Map<BookingDTO, Booking>(bookingDto);
            _unitOfWork.Bookings.Create(booking);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}