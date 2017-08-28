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
    public class FeedbackService : IFeedbackService
    {
        // IUnitOfWork object communicates with DAL 
        private IUnitOfWork _unitOfWork { get; set; }

        // Use DI to pass implementation of IUnitOfWork
        public FeedbackService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        // Get all feedbacks
        public IEnumerable<FeedbackDTO> GetFeedbacks()
        {
            var feedbacks = _unitOfWork.Feedbacks.GetAll().ToList();
            return Mapper.Map<List<Feedback>, List<FeedbackDTO>>(feedbacks);
        }

        // Get feedbacks for specific hotel
        public IEnumerable<FeedbackDTO> GetFeedbacksByHotelId(Guid Id)
        {
            var allFeedbacks = _unitOfWork.Feedbacks.GetAll().ToList();
            var feedbacks = (from b
                            in allFeedbacks
                            where b.HotelId==Id
                           select b
                           ).ToList();
            return Mapper.Map<List<Feedback>, List<FeedbackDTO>>(feedbacks);
        }

        // Get feedbackDto from Web, create feedback object and save to database
        public void AddFeedback(FeedbackDTO feedbackDto)
        {
             Feedback feedback = Mapper.Map<FeedbackDTO, Feedback>(feedbackDto);
            _unitOfWork.Feedbacks.Create(feedback);
            _unitOfWork.Save();
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}