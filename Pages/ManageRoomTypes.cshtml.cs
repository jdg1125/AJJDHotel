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

        public ManageRoomTypesModel(IDbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
            RoomTypeList = new List<RoomType>();
        }

        public void OnGet()
        {
            RoomTypeList = dbAccess.GetRoomTypes();

        }
    }
}
