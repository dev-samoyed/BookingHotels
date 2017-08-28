using BookingHotels.BLL.DTO;
using System;
using System.Collections.Generic;

namespace BookingHotels.BLL.Interfaces
{
    public interface IFeedbackService
    {
        // Get all feedbacks
        IEnumerable<FeedbackDTO> GetFeedbacks();
        // Get all feedbacks for particular hotel
        IEnumerable<FeedbackDTO> GetFeedbacksByHotelId(Guid Id);
        void AddFeedback(FeedbackDTO feedbackDto);
        void Dispose();
    }
}

