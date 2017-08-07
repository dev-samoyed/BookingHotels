using BookingHotels.Domain.Repositories;
using Microsoft.AspNet.Identity;

namespace BookingHotels.Domain.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }
    }
}
