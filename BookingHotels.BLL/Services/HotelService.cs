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
    public class HotelService : IHotelService
    {
        // IUnitOfWork object communicates with DAL 
        private IUnitOfWork _unitOfWork { get; set; }
        // Use DI to pass implementation of IUnitOfWork
        public HotelService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        // Get all hotels
        public IEnumerable<HotelDTO> GetHotels()
        {
            var hotels = _unitOfWork.Hotels.GetAll().ToList();
            return Mapper.Map<List<Hotel>, List<HotelDTO>>(hotels);
        }
        // Gets hotel by it's Id
        public HotelDTO GetHotelById(Guid Id)
        {
            Hotel hotel = _unitOfWork.Hotels.Get(Id);
            return Mapper.Map<Hotel, HotelDTO>(hotel);
        }
        // Add a new hotel
        public void AddHotel(HotelDTO hotelDto)
        {
            Hotel hotel = Mapper.Map<HotelDTO, Hotel>(hotelDto);
            _unitOfWork.Hotels.Create(hotel);
            _unitOfWork.Save();
        }
        // Delete hotel
        public void DeleteHotel(HotelDTO hotelDto)
        {
            Hotel hotel = Mapper.Map<HotelDTO, Hotel>(hotelDto);
            _unitOfWork.Hotels.Delete(hotel.Id);
            _unitOfWork.
            Save();
        }
        // Dispose
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
