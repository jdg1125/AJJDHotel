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

        public List<string> BedTypes { get; set; }
        public List<string> ViewTypes { get; set; }

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
            BedTypes = new List<string>();

            checkin = new DateTime();
            checkout = new DateTime();

            _userManager = userManager;

        }

        public void OnGet(DateTime start, DateTime end)
        {

            checkin = start;
            checkout = end;

            BedTypes = dbAccess.GetDistinctBeds();
            ViewTypes = dbAccess.GetDistinctViews();

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
            if (User.Identity.IsAuthenticated){
                return RedirectToPage("OrderSummary", new { id = id });
            }
            else{
                return RedirectToPage("/Account/Login", new { Area = "Identity", returnUrl = $"~/SearchResults/{Request.QueryString}" });
            }

        }
        public IActionResult OnPost(){         // for using the date pick on the search results page
            TempData["checkin"]=checkin;
            TempData["checkout"]=checkout;
            TempData["numGuests"]=numGuests;
            return RedirectToPage("SearchResults", new { start = checkin, end = checkout });

        }


    }
}