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
            CreateMap<Hotel, HotelDTO>(MemberList.None);
            CreateMap<Room, RoomDTO>(MemberList.None);
        }
    }
}
