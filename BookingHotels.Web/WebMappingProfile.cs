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

            //RoomDTO -> RoomViewModel
            Mapper.Initialize(cfg => cfg.CreateMap<RoomDTO, RoomViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<HotelDTO, HotelViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<FeedbackDTO, FeedbackViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<BookingDTO, BookingViewModel>());
        }
    }

}