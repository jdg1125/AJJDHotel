using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AJJDHotel.Models
{
    public class RoomType
    {
        //should room type have a location? weird to have room type with ocean view in Kansas City
        public int Id { get; set; }
        public string Description { get; set; }
        public string Beds { get; set; }
        public string View { get; set; }
        public string RoomName { get; set; }
        public decimal Rate { get; set; }
        public string ImgPath { get; set; }
    }
}
