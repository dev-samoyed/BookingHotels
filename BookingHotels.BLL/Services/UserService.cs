using BookingHotels.BLL.DTO;
using BookingHotels.BLL.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using BookingHotels.BLL.Interfaces;
using BookingHotels.Domain.Interfaces;
using System.Linq;
using System;
using BookingHotels.Domain.Entities;

namespace BookingHotels.BLL.Services
{
    public class UserService : IUserService
    {
        // IUnitOfWork object communicates with DAL 
        IUnitOfWork _unitOfWork { get; set; }
        // Use DI to pass implementation of IUnitOfWork
        public UserService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        // Get user by his Id
        public ApplicationUser GetUserById(Guid Id) {
            ApplicationUser user = _unitOfWork.ApplicationUserManager.FindById(Id);
            return user;
        }
        // Create
        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await _unitOfWork.ApplicationUserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email};
                var result = await _unitOfWork.ApplicationUserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // Fill UserRoles table
                await _unitOfWork.ApplicationUserManager.AddToRoleAsync(user.Id, userDto.Role);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, "Registration succesfull", "");
            }
            else
            {
                return new OperationDetails(false, "User with this name already exists", "Email");
            }
        }
        // Authenticate
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // Find user
            ApplicationUser user = await _unitOfWork.ApplicationUserManager.FindAsync(userDto.Email, userDto.Password);
            // Authorize him and return ClaimsIdentity obj
            if (user != null)
                claim = await _unitOfWork.ApplicationUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}