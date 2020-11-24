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

            int numGuestsP = 2;
            int roomIdP = 10;
            decimal totalChargeP = 200.00M;
            Reservation myRes = new Reservation() { StartDate = tempStartDate, EndDate = tempEndDate, NumGuests = 2, RoomId = 10, TotalCharge = 200.00M, Id = "41ab7152-18a7-4d7b-905c-928d7bd1efc8" };
            // dbAccess.CreateReservation(myRes);

        }


    }
}