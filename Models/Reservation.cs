using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AJJDHotel.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        [Required]
        public int GuestId { get; set; }  // TODO Link with IdentityUser (custom) class
        [Required]
        public Room Room { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [Column(TypeName="decimal")]
        public decimal TotalCharge { get; set; }
        [Required]
        public int NumGuests { get; set; }
    }
}
