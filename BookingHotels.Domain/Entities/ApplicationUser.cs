using BookingHotels.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotels.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid, CustomUserLogin, CustomUserRole, 
    CustomUserClaim> 
    {
        public ApplicationUser() {
            Id = Guid.NewGuid();
        }
        // These properties will be converted to columns in table “ApplicationUsers”
        [Key]
        // Id is generated in the constructor above 
        public override Guid Id { get; set; }
    }
}
