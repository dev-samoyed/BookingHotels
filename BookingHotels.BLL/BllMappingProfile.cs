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

            // RoomDTO -> RoomViewModel
            // BookingHotels.BLL.DTO.RoomDTO->BookingHotels.Web.Models.RoomViewModel

            // Crea te map (source is Hotel entity, destination is Hotel DTO)
            CreateMap<Hotel, HotelDTO>(MemberList.None);
            CreateMap<Room, RoomDTO>(MemberList.None);

            //CreateMap<RoomDTO, BookingHotels.Web.Models.RoomViewModel>(MemberList.None);
        }
    }
}
