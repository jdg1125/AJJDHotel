using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Data;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AJJDHotel.Pages
{
    public class MyReservationsModel : PageModel
    {
        private readonly IDbAccess dbAccess;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUser ApplicationUser { get; set; }
        public List<Reservation> UsersReservations { get; set; }

        public int num { get; set; }



        public MyReservationsModel(IDbAccess dbAccess, UserManager<ApplicationUser> userManager)
        {
            this.dbAccess = dbAccess;
            UsersReservations = new List<Reservation>();
            _userManager = userManager;
        }

        public void OnGet()
        {
            string id = _userManager.GetUserId(User);
            ApplicationUser = dbAccess.GetUserById(id);
            UsersReservations = dbAccess.GetReservationsByUserId(id);
        }
        public void OnGetCancel(int id)
        {
            num = id;
            this.OnGet();
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