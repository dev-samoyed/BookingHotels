using BookingHotels.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.BLL.Services
{
    class ApplicationAddToRoleAsync<ApplicationUser, Guid> : UserManager<ApplicationUser, Guid>
    {
        public ApplicationAddToRoleAsync(IUserStore<ApplicationUser, Guid> store) : base(store)
        {
        }
    }
}
