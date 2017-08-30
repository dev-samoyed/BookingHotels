using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.WebAPI.Models
{
    public class RoomImageUploadModel
    {
        public Guid Id { get; set; }
        public List<byte[]> Images { get; set; }
        public Guid RoomId { get; set; }
    }
}
