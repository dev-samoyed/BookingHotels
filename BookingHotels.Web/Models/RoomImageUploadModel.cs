using System;
using BookingHotels.Domain.Entities;
using System.Collections.Generic;

namespace BookingHotels.Web.Models
{
    public class RoomImagesUploadModel
    {
        public List<Guid> Id { get; set; }
        // Image data to upload
        public List<byte[]> Images { get; set; }
        public Guid RoomId { get; set; }
        // Link to the room pictured on this image
        public virtual Room Room { get; set; }

    }
}