﻿using BookingHotels.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotels.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid, CustomUserLogin, CustomUserRole, 
    CustomUserClaim> 
    {

        // These properties will be converted to columns in table “ApplicationUsers”
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public new Guid Id { get; set; }
    }
}