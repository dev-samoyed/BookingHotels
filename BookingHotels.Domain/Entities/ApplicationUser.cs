using Microsoft.AspNet.Identity.EntityFramework;

namespace BookingHotels.DAL.Repositories
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
