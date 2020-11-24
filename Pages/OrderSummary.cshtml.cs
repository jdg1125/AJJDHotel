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

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public int NumGuests { get; set; }

        public int RoomTypeId { get; set; }

        public Room Room { get; set; }



        public RoomType RoomType { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public decimal TotalCharge { get; set; }

        // for the Id of logged in user
        public string Id { get; set; }


        public OrderSummaryModel(IDbAccess dbAccess, UserManager<ApplicationUser> userManager)
        {
            this.dbAccess = dbAccess;
            _userManager = userManager;
            this.RoomType = new RoomType();
            this.Room = new Room();
            StartDate = new DateTime();
            EndDate = new DateTime();
            
        }

        public void OnGet(DateTime start, DateTime end, int id)
        {

            StartDate = start;
            EndDate = end;
            RoomTypeId = id;

            // get the logged in user's id
            Id = _userManager.GetUserId(User);

            ApplicationUser = dbAccess.GetUserById(Id);

            // used to find TotalCharge
            double nights = (EndDate - StartDate).TotalDays;

            // gets room to get room rate of desired room to calculate total charge for CreateReservation and display total charge here
            // TODO these two dbAccess queries are super wasteful, need better ones (projections)
            RoomType = dbAccess.GetRoomTypeByRoomTypeId(id);

            TotalCharge = (decimal)nights * RoomType.Rate;

            // gets first available room that has the desired room type id (need this to get room id for CreateReservation)
            Room = dbAccess.GetAvailableRoomByRoomTypeId(id, StartDate, EndDate);


        }

        public IActionResult OnPost(DateTime startdate, DateTime enddate, int numguests, int roomid, decimal totalcharge, string id)
        {

            // CreateReservation method returns the PK of the newly-created Reservation; use it to get confirmation number
            int resId = dbAccess.CreateReservation(startdate, enddate, numguests, roomid, totalcharge, id);

            

            return RedirectToPage("/OrderConfirmation", new { reservationId = resId });
        }

        // possible helper method
        public decimal GetTotalCharge(int roomTypeId)
        {
            throw new NotImplementedException();
        }

        



    }
}
