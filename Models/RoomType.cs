using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AJJDHotel.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }

        [MaxLength(280)]
        [Required]
        public string Description { get; set; }

        [MaxLength(50)]
        [Required]
        public string Beds { get; set; }
        [MaxLength(50)]
        [Required]
        public string View { get; set; }
        [MaxLength(50)]
        [Required]
        public string RoomName { get; set; }
        [Required]
        [Column(TypeName = "decimal")]

        public decimal Rate { get; set; }
        [MaxLength(150)]
        [Required]
        public string ImgPath { get; set; }
    }
}
