using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BookingHotels.Web.Models
{
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