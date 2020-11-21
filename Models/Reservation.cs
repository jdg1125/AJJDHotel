using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AJJDHotel.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        
        
        public Room Room { get; set; }
        [Required]
        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [Column(TypeName = "decimal")]
        public decimal TotalCharge { get; set; }
        [Required]
        public int NumGuests { get; set; }


        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

    }
}