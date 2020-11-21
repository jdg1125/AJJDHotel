using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AJJDHotel.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        [Required]
        public bool IsAdmin { get; set; }

        [Required]    
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
