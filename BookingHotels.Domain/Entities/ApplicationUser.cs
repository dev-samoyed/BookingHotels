using Microsoft.AspNet.Identity.EntityFramework;

namespace BookingHotels.Domain.Repositories
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
