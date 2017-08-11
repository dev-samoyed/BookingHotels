using AutoMapper;
using BookingHotels.BLL.DTO;
using BookingHotels.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingHotels.Web
{

    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            //DTO (BLL) -> ViewModel (WEB)
            CreateMap<RoomDTO, RoomViewModel>(MemberList.None);
            CreateMap<HotelDTO, HotelViewModel>(MemberList.None);
            CreateMap<FeedbackDTO, FeedbackViewModel>(MemberList.None);
            CreateMap<BookingDTO, BookingViewModel>(MemberList.None);
            CreateMap<HotelViewModel, HotelDTO>();
            CreateMap<RoomViewModel, RoomDTO>();
        }
    }

}