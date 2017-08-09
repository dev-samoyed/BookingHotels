using Microsoft.AspNet.Identity.EntityFramework;

namespace BookingHotels.Domain.Repositories
{
    public class ApplicationUser : IdentityUser
    {
        // todo: Make This Primary User Entity
        // public Guid Id { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }



    }
}
