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

        [BindProperty, TempData]
        public DateTime StartDate { get; set; }

        [BindProperty, TempData]
        public DateTime EndDate { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;




        public SearchResultsModel(IDbAccess dbAccess, UserManager<ApplicationUser> userManager)
        {
            this.dbAccess = dbAccess;
            AvailableRoomTypes = new List<RoomType>();

            StartDate = new DateTime();
            EndDate = new DateTime();

            _userManager = userManager;

        }

        public void OnGet(DateTime start, DateTime end)
        {

            StartDate = start;
            EndDate = end;

            if (StartDate < EndDate)
            {
                AvailableRoomTypes = dbAccess.GetAvailableRoomTypes(StartDate, EndDate);
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


    }
}