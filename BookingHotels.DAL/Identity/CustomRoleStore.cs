using BookingHotels.DAL.EF;
using BookingHotels.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace BookingHotels.DAL.Identity
{
    public class CustomRoleStore : RoleStore<CustomRole, Guid, CustomUserRole>
    {
        public CustomRoleStore(MyDbContext context)
            : base(context)
        {
        }
    }
}
