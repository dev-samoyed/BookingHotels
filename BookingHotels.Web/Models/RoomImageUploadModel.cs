using System;
using BookingHotels.Domain.Enums;
using BookingHotels.Domain.Entities;
using System.Collections.Generic;

namespace BookingHotels.Web.Models
{
    public class RoomImageUploadModel
    {
        public Guid Id { get; set; }
        // Image data to upload
        public byte[] Image { get; set; }
        public Guid RoomId { get; set; }
        // Link to the room pictured on this image
        public virtual Room Room { get; set; }

    }
}