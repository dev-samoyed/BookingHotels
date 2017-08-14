using System;
using System.Threading.Tasks;
using BookingHotels.Domain.Identity;

namespace BookingHotels.Domain.Interfaces
{
    public interface IUnitOfWorkIdentity : IDisposable
    {
        ApplicationUserManager ApplicationUserManager { get; }
        ApplicationRoleManager ApplicationRoleManager { get; }
        Task SaveAsync();
    }
}
