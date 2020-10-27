using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AJJDHotel.Models
{
    public class Reservation
    {
        public string ConfirmationNum { get; set; }
        public string RoomNum { get; set; }
        public string Location { get; set; }
        public DateTime Checkin { get; set; }

        public DateTime Checkout { get; set; }
        public double TotalCharge { get; set; }

        public string NameFirstLast { get; set; }
        public string RoomType { get; set; }
    }
}
