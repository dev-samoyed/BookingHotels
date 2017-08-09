using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace BookingHotels.Domain.Entities
{
    public class ApplicationUserRoles : IdentityUserRole
    {
        //
        // Summary:
        //     RoleId for the role
        public new Guid  RoleId { get; set; }
        //
        // Summary:
        //     UserId for the user that is in the role
        public new Guid UserId { get; set; }

        //List<ApplicationUser> applicationUser;
        //ApplicationRole applicationUserRole;
    }
}