using AutoMapper;
using BookingHotels.BLL.DTO;
using BookingHotels.Domain.Identity;
using BookingHotels.Web.Models;

namespace BookingHotels.Web
{

    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<RoomViewModel, RoomDTO>(MemberList.None);
            // Add config for RoomType enum => string
            CreateMap<RoomDTO, RoomViewModel>(MemberList.None)
            .ForMember(o => o.RoomType, b => b.MapFrom(z => z.RoomType));

            CreateMap<HotelDTO, HotelViewModel>(MemberList.None);
            CreateMap<HotelViewModel, HotelDTO>(MemberList.None);

            CreateMap<FeedbackDTO, FeedbackViewModel>(MemberList.None);
            CreateMap<FeedbackViewModel, FeedbackDTO>(MemberList.None);

            CreateMap<BookingDTO, BookingViewModel>(MemberList.None);
            CreateMap<BookingViewModel, BookingDTO>(MemberList.None);

            CreateMap<RoomImageUploadModel, RoomImageDTO>(MemberList.None);
            CreateMap<RoomImageDTO, RoomImageUploadModel>(MemberList.None);

            CreateMap<CustomUserRegister, UserDTO>(MemberList.None);
        }
    }

}