using BookingHotels.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookingHotels.Web.Models
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public Guid RoomId { get; set; }
        [Required]
        public Guid ApplicationUserId { get; set; }
        [Required]
        public DateTime BookingStartDate { get; set; }
        [Required]
        public DateTime BookingEndDate { get; set; }
        // Links to user and room he has booked
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Room Room { get; set; }
    }
}