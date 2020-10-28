using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AJJDHotel.Pages
{
    public class PurchaseModel : PageModel
    {
        public int RoomChosen { get; set; }
        public List<RoomType> RoomsAvailable { get; set; }
        public void OnGet(int id)
        {
            SearchResultsModel dataModel = new SearchResultsModel();
            RoomsAvailable = dataModel.Results;
            RoomChosen = id;
        }
    }
}
