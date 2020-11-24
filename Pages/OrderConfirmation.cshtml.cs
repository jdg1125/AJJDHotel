using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AJJDHotel.Pages
{
    public class OrderConfirmationModel : PageModel
    {

        [BindProperty]
        public DateTime StartDate { get; set; }

        public Reservation Reservation { get; set; }




        public void OnGet(int reservationId)
        {
            int confirmationNumber = MakeConfirmationNumber(reservationId);


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
