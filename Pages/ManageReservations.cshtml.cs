using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AJJDHotel.Utility;
using AJJDHotel.Data;

namespace AJJDHotel.Pages
{
    [Authorize(Roles = SD.AdminUser)]
    public class ManageReservationsModel : PageModel
    {
        public List<Reservation> QueryByEmail { get; set; }
        public Reservation QueryByResNum { get; set; }

        public string ResNumber { get; set; }
        public string GuestEmail { get; set; }

        private IDbAccess _dbAccess;
        public bool WasSuccess { get; set; }
        public ManageReservationsModel(IDbAccess dbAccess)
        {
            WasSuccess = true;
            _dbAccess = dbAccess;
        }

        public void OnGet(string resNumber, string guestEmail)
        {
            ResNumber = resNumber;
            GuestEmail = guestEmail;

            if (ResNumber != null)
            {
                if ((WasSuccess = Int32.TryParse(ResNumber, out int numAsInt)))
                    WasSuccess = (QueryByResNum = _dbAccess.GetReservationByConfirmation(numAsInt)) == null ? false : true;
            }
            else if (GuestEmail != null)
            {
                ApplicationUser user;
                if ((WasSuccess = (user = _dbAccess.GetUserByEmail(GuestEmail)) != null))
                    WasSuccess = (QueryByEmail = _dbAccess.GetReservationsByUserId(user.Id)).Count == 0 ? false : true;
            }
            else
                WasSuccess = false;
        }
    }
}
