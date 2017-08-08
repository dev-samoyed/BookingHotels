using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotels.Domain.Repositories
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}