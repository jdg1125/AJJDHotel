using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AJJDHotel.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; }
    }
}
