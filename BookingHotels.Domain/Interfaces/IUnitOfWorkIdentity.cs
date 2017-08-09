using System;
using System.Threading.Tasks;
using BookingHotels.Domain.Identity;

namespace BookingHotels.Domain.Interfaces
{
    public interface IUnitOfWorkIdentity : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}

