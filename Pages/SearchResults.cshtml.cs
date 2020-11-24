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

        public DateTime tempStartDate;
        public DateTime tempEndDate;

        private readonly UserManager<ApplicationUser> _userManager;




        public SearchResultsModel(IDbAccess dbAccess, UserManager<ApplicationUser> userManager)
        {
            this.dbAccess = dbAccess;
            AvailableRoomTypes = new List<RoomType>();
            tempStartDate = new DateTime();
            tempEndDate = new DateTime();

            _userManager = userManager;

        }

        public void OnGet(DateTime start, DateTime end)
        {

            tempStartDate = new DateTime(2020, 12, 06);
            tempEndDate = new DateTime(2020, 12, 10);

            if (tempStartDate < tempEndDate)
            {
                AvailableRoomTypes = dbAccess.GetAvailableRoomTypes(tempStartDate, tempEndDate);
            }
            else
            {
                throw new Exception();
            }

            string userId = User.Identity.Name;
            string userId2 = _userManager.GetUserId(User);

        }

        public IActionResult OnGetReserve(int id)
        {
            return RedirectToPage("OrderSummary", new { start = tempStartDate, end = tempEndDate, id = id });
        }


    }
}