using System;
using System.Collections.Generic;
using System.Net;

namespace BookingHotels.Web.Models
{
    public class ImageUploadResult
    {
        public Guid[] Id;
        public HttpStatusCode HttpStatusCode;
    }
}