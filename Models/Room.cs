using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AJJDHotel.Models
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public int RoomId { get; set; }
        public RoomType RoomType { get; set; }
        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }
    }
}
