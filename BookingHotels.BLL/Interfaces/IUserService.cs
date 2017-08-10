using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookingHotels.BLL.DTO;
using BookingHotels.BLL.Infrastructure;
 
namespace BookingHotels.BLL.Interfaces
{
    // Through this interface objects from Web will communicate with DAL
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
