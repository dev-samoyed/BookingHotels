using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.WebAPI.Models
{
    // Object to sent as post result
    public class ImageUploadResult
    {
        public Guid Id;
        public HttpStatusCode HttpStatusCode;
        public ImageUploadResult()
        {
            Id = Guid.NewGuid();
        }
    }
}
