using BookingHotels.DAL.EF;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace BookingHotels.DAL.Identity
{
    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(MyDbContext context)
            : base(context)
        {
        }
    }
}
