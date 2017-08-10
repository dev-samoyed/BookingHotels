using BookingHotels.DAL.EF;
using BookingHotels.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace BookingHotels.DAL.Identity
{
    // Extend Identity classes to specify a Guid for the key
    public class CustomRoleStore : RoleStore<CustomRole, Guid, CustomUserRole>
    {
        public CustomRoleStore(MyDbContext context)
            : base(context)
        { }
    }
}
