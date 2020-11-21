using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AJJDHotel.Models
{
    public class Room
    {
        [Required]
        public int RoomNumber { get; set; }
        [Key]
        public int RoomId { get; set; }
        
        public RoomType RoomType { get; set; }        
        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }
    }
}
