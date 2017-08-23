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
    public class RoomImageService : IRoomImageService
    {
        // IUnitOfWork object communicates with DAL 
        private IUnitOfWork _unitOfWork { get; set; }
        // Use DI to pass implementation of IUnitOfWork
        public RoomImageService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        // Get all images for all rooms
        public IEnumerable<RoomImageDTO> GetRoomImages()
        {
            var roomImages = _unitOfWork.RoomImages.GetAll().ToList();
            return Mapper.Map<List<RoomImage>, List<RoomImageDTO>>(roomImages);
        }
        // Get images for specific room
        public IEnumerable<RoomImageDTO> GetRoomImagesByRoomId(Guid Id)
        {
            var allRoomImages = _unitOfWork.RoomImages.GetAll().ToList();
            var roomImages = (from b
                            in allRoomImages
                           where b.RoomId==Id
                           select b
                           ).ToList();
            return Mapper.Map<List<RoomImage>, List<RoomImageDTO>>(roomImages);
        }
        public void Create(RoomImageDTO roomImageDto)
        {
            RoomImage roomImage = Mapper.Map<RoomImageDTO, RoomImage>(roomImageDto);
            _unitOfWork.RoomImages.Create(roomImage);
            _unitOfWork.Save();
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}