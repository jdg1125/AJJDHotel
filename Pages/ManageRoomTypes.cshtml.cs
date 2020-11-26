using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Data;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AJJDHotel.Pages
{
    public class ManageRoomTypesModel : PageModel
    {

        private readonly IDbAccess dbAccess;

        public List<RoomType> RoomTypeList { get; set; }

        public Room Room { get; set; }

        [BindProperty]
        public int RoomTypeId { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string Beds { get; set; }
        [BindProperty]
        public string View { get; set; }
        [BindProperty]
        public string RoomName { get; set; }
        [BindProperty]
        public decimal Rate { get; set; }
        [BindProperty]
        public string ImgPath { get; set; }


        public ManageRoomTypesModel(IDbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
            RoomTypeList = new List<RoomType>();
            Room = new Room();
        }

        public void OnGet()
        {
            RoomTypeList = dbAccess.GetRoomTypes();
        }

        public IActionResult OnPost()
        {
            dbAccess.UpdateRoomType(RoomTypeId, Description, Beds, View, RoomName, Rate, ImgPath);
            return new RedirectToPageResult("ManageRoomTypes");
        }
    }
}
