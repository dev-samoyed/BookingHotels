using AutoMapper;
using BookingHotels.BLL.DTO;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Identity;

namespace BookingHotels.BLL
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateMap<Hotel, HotelDTO>(MemberList.None).ReverseMap(); 

            CreateMap<Room, RoomDTO>(MemberList.None).ReverseMap();

            CreateMap<Feedback, FeedbackDTO>(MemberList.None).ReverseMap();

            CreateMap<Booking, BookingDTO>(MemberList.None).ReverseMap();

            CreateMap<RoomImage, RoomImageDTO>(MemberList.None).ReverseMap();
        }
    }
}
