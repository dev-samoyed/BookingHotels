using AutoMapper;
using BookingHotels.BLL.DTO;
using BookingHotels.Web.Models;

namespace BookingHotels.Web
{

    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<RoomDTO, RoomViewModel>(MemberList.None);
            CreateMap<RoomViewModel, RoomDTO>();

            CreateMap<HotelDTO, HotelViewModel>(MemberList.None);
            CreateMap<HotelViewModel, HotelDTO>();

            CreateMap<FeedbackDTO, FeedbackViewModel>(MemberList.None);
            CreateMap<FeedbackViewModel, FeedbackDTO>(MemberList.None);

            CreateMap<BookingDTO, BookingViewModel>(MemberList.None);
            CreateMap<BookingViewModel, BookingDTO>(MemberList.None);


        }
    }

}