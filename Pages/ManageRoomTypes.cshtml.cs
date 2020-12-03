using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Data;
using AJJDHotel.Models;
using AJJDHotel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AJJDHotel.Pages
{
    [Authorize(Roles = SD.AdminUser)]

    public class ManageRoomTypesModel : PageModel
    {

        private readonly IDbAccess dbAccess;

        public List<RoomType> RoomTypeList { get; set; }

        public Room Room { get; set; }

        [BindProperty]
        public int Index { get; set; }
        [BindProperty]
        public List<int> RoomTypeId { get; set; }
        [BindProperty]
        public List<string> Description { get; set; }
        [BindProperty]
        public List<string> Beds { get; set; }
        [BindProperty]
        public List<string> View { get; set; }
        [BindProperty]
        public List<string> RoomName { get; set; }
        [BindProperty]
        public List<decimal> Rate { get; set; }
        [BindProperty]
        public List<string> ImgPath { get; set; }


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
            dbAccess.UpdateRoomType(RoomTypeId[Index], Description[Index], Beds[Index], View[Index], RoomName[Index], Rate[Index], ImgPath[Index]);
            return new RedirectToPageResult("ManageRoomTypes");
        }
    }
}
