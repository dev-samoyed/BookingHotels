using BookingHotels.BLL.DTO;
using BookingHotels.BLL.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using BookingHotels.BLL.Interfaces;
using BookingHotels.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Identity;

namespace BookingHotels.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWorkIdentity _unitOfWork { get; set; }

        public UserService(IUnitOfWorkIdentity uow)
        {
            _unitOfWork = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await _unitOfWork.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email};
                var result = await _unitOfWork.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                //await _unitOfWork.UserManager.AddToRoleAsync(user.Id.ToString(), userDto.Role);
                await _unitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, "Registration succesfull", "");
            }
            else
            {
                return new OperationDetails(false, "User with this name already exists", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // Find user
            ApplicationUser user = await _unitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);
            // Authorize him and return ClaimsIdentity obj
            if (user != null)
                claim = await _unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // Initialize DB
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _unitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new CustomRole { Name = roleName };
                    await _unitOfWork.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}