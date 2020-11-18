﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AJJDHotel.Models
{
    public class ApplicationUser : IdentityUser
    {
       

        public bool IsAdmin { get; set; }

        [Required]
        [Range(18, 160)]
        public int Age { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        

        public List<Reservation> Reservations { get; set; }


    }
}
