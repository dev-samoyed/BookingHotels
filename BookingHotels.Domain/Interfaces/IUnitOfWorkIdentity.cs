using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.DAL.Interfaces;
using BookingHotels.Domain.Identity;

namespace BookingHotels.Domain.Interfaces
{
    public interface IUnitOfWorkIdentity : IDisposable
    {

        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}

