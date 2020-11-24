using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Data;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AJJDHotel.Utility;

namespace AJJDHotel.Pages
{
    public class OrderSummaryModel : PageModel
    {
        private readonly IDbAccess dbAccess;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public int NumGuests { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RoomTypeId { get; set; }

        public Room Room { get; set; }

        public RoomType RoomType { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public decimal TotalCharge { get; set; }

        // for the Id of logged in user
        public string Id { get; set; }

        public int ConfirmationNumber { get; set; }

        public DateTime tempStartDate;
        public DateTime tempEndDate;

        public OrderSummaryModel(IDbAccess dbAccess, UserManager<ApplicationUser> userManager)
        {
            this.dbAccess = dbAccess;
            _userManager = userManager;
            this.RoomType = new RoomType();
            this.Room = new Room(); 
            StartDate = new DateTime();
            EndDate = new DateTime();
            

        }

        public void OnGet(DateTime startDate, DateTime endDate, int roomTypeIdP)
        {
            // these are hardcoded until actual data is available (possibly data will not be OnPost parameters)
            StartDate = new DateTime(2020, 12, 06);
            EndDate = new DateTime(2020, 12, 10);

            // get the logged in user's id
            Id = _userManager.GetUserId(User);

            ApplicationUser = dbAccess.GetUserById(Id);

            // used to find TotalCharge
            double nights = (EndDate - StartDate).TotalDays;

            TotalCharge = (decimal)nights * RoomType.Rate;

            // gets first available room that has the desired room type id (need Room to get room id for CreateReservation)
            Room = dbAccess.GetAvailableRoomByRoomTypeId(12, StartDate, EndDate);

            // gets room rate of desired room to calculate total charge for CreateReservation
            // TODO these two dbAccess queries are super wasteful, need better ones (projections)
            RoomType = dbAccess.GetRoomTypeByRoomTypeId(1);

        }

        public IActionResult OnPost(DateTime startDate, DateTime endDate, int roomTypeIdP)
        {

            // CreateReservation method returns the PK of the newly-created Reservation; use it to get confirmation number
            int myPK = dbAccess.CreateReservation(StartDate, EndDate, NumGuests, Room.RoomId, TotalCharge, Id);

            ConfirmationNumber = MakeConfirmationNumber(myPK);

            return RedirectToPage("/OrderConfirmation");
        }

        // possible helper method
        public decimal GetTotalCharge(int roomTypeId)
        {
            throw new NotImplementedException();
        }

        public int MakeConfirmationNumber(int pk)
        {
            return 8744304 + pk;
        }

        public int ConfirmationNumberToPK(int confirmation)
        {
            // return -1 to indicate an invalid confirmation number
            if (confirmation < 8744305)
            {
                return -1;
            }
            return confirmation - 8744304;
        }



    }
}
