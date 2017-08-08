using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookingHotels.BLL.DTO;
using BookingHotels.BLL.Infrastructure;
 
namespace BookingHotels.BLL.Interfaces
{
    // Через объекты данного интерфейса web будет 
    // взаимодействовать с уровнем доступа к данным. 
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
