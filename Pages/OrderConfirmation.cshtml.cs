using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Models;
using AJJDHotel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AJJDHotel.Pages
{
    public class OrderConfirmationModel : PageModel
    {

        private readonly IDbAccess dbAccess;

        [BindProperty]
        public DateTime StartDate { get; set; }

        public Reservation Reservation { get; set; }
        public RoomType RoomType { get; set; }
        public int ConfirmationNumber { get; set; }

        public OrderConfirmationModel(IDbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
            Reservation = new Reservation();
        }


        public void OnGet(int reservationId, int roomTypeId)
        {
            ConfirmationNumber = MakeConfirmationNumber(reservationId);

            Reservation = dbAccess.GetReservationByConfirmationNumber(ConfirmationNumber);

            RoomType = dbAccess.GetRoomTypeByRoomTypeId(roomTypeId);
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
