using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AJJDHotel.Models;
using AJJDHotel.Data;
using AJJDHotel.Utility;
using Microsoft.AspNetCore.Identity;


namespace AJJDHotel.Pages
{
    public class SearchResultsModel : PageModel
    {
        public List<RoomType> AvailableRoomTypes { get; set; }

        private readonly IDbAccess dbAccess;

        [BindProperty]
        public DateTime checkin { get; set; }

        [BindProperty]
        public DateTime checkout { get; set; }
        [BindProperty]
        public int numGuests { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;




        public SearchResultsModel(IDbAccess dbAccess, UserManager<ApplicationUser> userManager)
        {
            this.dbAccess = dbAccess;
            AvailableRoomTypes = new List<RoomType>();

            checkin = new DateTime();
            checkout = new DateTime();

            _userManager = userManager;

        }

        public void OnGet(DateTime start, DateTime end)
        {

            checkin = start;
            checkout = end;

            if (checkin < checkout)
            {
                AvailableRoomTypes = dbAccess.GetAvailableRoomTypes(checkin, checkout);
            }
            else
            {
                throw new Exception();
            }
        }


        public IActionResult OnGetReserve(int id)
        {
            return RedirectToPage("OrderSummary", new { id = id });
        }
        // for using the date pick on the search results page
        public IActionResult OnPost(){
            TempData["checkin"]=checkin;
            TempData["checkout"]=checkout;
            TempData["numGuests"]=numGuests;
            return Page();

        }


    }
}