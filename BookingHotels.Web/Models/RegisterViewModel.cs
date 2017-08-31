using System;
using System.ComponentModel.DataAnnotations;

namespace BookingHotels.Web.Models
{
    // Register model
    public class RegisterViewModel
    {
            [Key]
            public Guid Id { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
    }
}