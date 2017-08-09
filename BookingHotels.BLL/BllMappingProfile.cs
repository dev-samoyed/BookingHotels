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
            // Entity (DAL) -> DTO (BLL)
            CreateMap<Hotel, HotelDTO>(MemberList.None);
            CreateMap<HotelDTO, Hotel>(MemberList.None);
            CreateMap<Room, RoomDTO>(MemberList.None);
            CreateMap<Feedback, FeedbackDTO>(MemberList.None);
            CreateMap<Booking, BookingDTO>(MemberList.None);
            CreateMap<CustomUserRegister, UserDTO>(MemberList.None);
            CreateMap<UserDTO, CustomUserRegister>(MemberList.None);
        }
    }
}
