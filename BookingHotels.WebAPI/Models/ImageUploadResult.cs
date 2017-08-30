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
        public Guid[] Id;
        public HttpStatusCode HttpStatusCode;
        public ImageUploadResult(int imagesCount)
        {
            // Generate Ids for image names
            Guid[] id = new Guid[imagesCount];
            for (int i = 0; i < imagesCount; i++)
                id[i] = Guid.NewGuid();
            this.Id = id;

        }
    }

}
