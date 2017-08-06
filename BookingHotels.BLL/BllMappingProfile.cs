using AutoMapper;
using BookingHotels.BLL.DTO;
using BookingHotels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.BLL
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            // Entity (DAL) -> DTO (BLL)
            CreateMap<Hotel, HotelDTO>(MemberList.None);
            CreateMap<Room, RoomDTO>(MemberList.None);
            CreateMap<Feedback, FeedbackDTO>(MemberList.None);
            CreateMap<Booking, BookingDTO>(MemberList.None);
        }
    }
}
