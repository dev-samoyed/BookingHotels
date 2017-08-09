using System;
using System.ComponentModel.DataAnnotations;

namespace BookingHotels.Domain.Entities
{
    public class Login
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}