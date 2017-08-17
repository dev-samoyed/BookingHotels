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
        public List<object> IsRoomOccupied(Guid id, DateTime startDate1, DateTime endDate1)
        {
            List<object> result = new List<object>();
            // All bookings for this room
            var bookings = GetBookingsByRoom(id);
            // Check if room is already booked in that ranges 
            foreach (BookingDTO booking in bookings)
            {
                var startDate2 = booking.BookingStartDate;
                var endDate2 = booking.BookingEndDate;
                // Check date is within already booked date ranges
                if ((startDate2 >= startDate1 && startDate2 <= endDate1) ||
                    (endDate2 >= startDate1 && endDate2 <= endDate1))
                {
                    result.Add(true);
                    result.Add(booking.BookingStartDate);
                    result.Add(booking.BookingEndDate);

                    return result;
                    //return Content("Sorry, the room is occupied from " + booking.BookingStartDate + " to " + booking.BookingEndDate + "<a href='javascript: history.back()'>Go Back</a>");
                }
            }
            result.Add(false);
            return result;

        }

        public IEnumerable<BookingDTO> GetBookingsByRoom(Guid Id)
        {
            var allBookings = _unitOfWork.Bookings.GetAll().ToList();
            var bookings = (from b
                            in allBookings
                           where b.RoomId==Id
                           select b
                           ).ToList();
            return Mapper.Map<List<Booking>, List<BookingDTO>>(bookings);
        }
       

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