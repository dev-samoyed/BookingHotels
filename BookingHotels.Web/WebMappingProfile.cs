using AutoMapper;
using BookingHotels.BLL.DTO;
using BookingHotels.Domain.Enums;
using BookingHotels.Web.Models;
using System;
using System.Diagnostics;

namespace BookingHotels.Web
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<RoomViewModel, RoomDTO>(MemberList.None);

            CreateMap<RoomType, string>().ConvertUsing(s => Convert.ToString(s));
            CreateMap<RoomDTO, RoomViewModel>(MemberList.None);

            CreateMap<HotelDTO, HotelViewModel>(MemberList.None).ReverseMap();

            CreateMap<FeedbackDTO, FeedbackViewModel>(MemberList.None).ReverseMap();

            CreateMap<BookingDTO, BookingViewModel>(MemberList.None).ReverseMap();

            CreateMap<RegisterViewModel, UserDTO>(MemberList.None);
        }
    }

}