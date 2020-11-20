//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AJJDHotel.Models
//{
//    public class Reservation
//    {
//        public string ConfirmationNum { get; set; }
//        public string RoomNum { get; set; }
//        public string Location { get; set; }
//        public DateTime Checkin { get; set; }

//        public DateTime Checkout { get; set; }
//        public double TotalCharge { get; set; }

//        public string NameFirstLast { get; set; }
//        public string RoomType { get; set; }
//    }
//}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AJJDHotel.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        
        
        [Required]
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