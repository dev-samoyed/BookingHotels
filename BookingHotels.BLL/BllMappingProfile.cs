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
            CreateMap<Hotel, HotelDTO>(MemberList.None);
            CreateMap<HotelDTO, Hotel>(MemberList.None);

            CreateMap<Room, RoomDTO>(MemberList.None);
            CreateMap<RoomDTO, Room>(MemberList.None);

            CreateMap<Feedback, FeedbackDTO>(MemberList.None);
            CreateMap<FeedbackDTO, Feedback>(MemberList.None);

            CreateMap<Booking, BookingDTO>(MemberList.None);
            CreateMap<BookingDTO, Booking>(MemberList.None);

            CreateMap<RoomImage, RoomImageDTO>(MemberList.None);
            CreateMap<RoomImageDTO, RoomImage>(MemberList.None);
        }
    }
}
