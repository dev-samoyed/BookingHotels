using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace BookingHotels.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public new Guid Id;
        // public new String Name;
    }
}