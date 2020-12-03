using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Models;
using AJJDHotel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace AJJDHotel.Pages
{
    [Authorize]
    public class OrderConfirmationModel : PageModel
    {
        private readonly IDbAccess dbAccess;

        [BindProperty]
        public DateTime StartDate { get; set; }

        public Reservation Reservation { get; set; }
        public RoomType RoomType { get; set; }
        public int ConfirmationNumber { get; set; }
        public string Password { get; set; }

        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }

        public OrderConfirmationModel(IDbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
            Reservation = new Reservation();
        }


        public void OnGet(int reservationId, int roomTypeId, string password, string firstName, string lastName)
        {
            Password = password;

            FirstName = firstName;
            LastName = lastName;

            ConfirmationNumber = MakeConfirmationNumber(reservationId);

            Reservation = dbAccess.GetReservationByConfirmationNumber(ConfirmationNumber);

            RoomType = dbAccess.GetRoomTypeByRoomTypeId(roomTypeId);

            AJJDEmailReservation(ConfirmationNumber);


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

        public void AJJDEmailReservation(int confirmationNumber)
        {
            MailAddress to = new MailAddress(User.Identity.Name);
            MailAddress from = new MailAddress("jtn2dsng@gmail.com");

            String image = RoomType.ImgPath;
            MailMessage message = new MailMessage(from, to);
            message.Subject = "AJJD Hotel Registration";
            string EmailReceipt = "Thank you for ordering at AJJD Hotel. Here is your email information: " +
            "\nConfirmationNumber: " + confirmationNumber +
            "\nDates of your stay: " + Reservation.StartDate.Date.ToString("D") + " - " + Reservation.EndDate.Date.ToString("D") +
            "\nTotal Charge: " + string.Format("{0:C}", Reservation.TotalCharge) +
            "\nRoom Name: " + RoomType.RoomName +
            "\nRate: " + string.Format("{0:C}", RoomType.Rate) +
            "\nDescription: " + RoomType.Description;
            message.Body = EmailReceipt;
            AlternateView confEmail = AlternateView.CreateAlternateViewFromString(
              $"{EmailReceipt} <br> <img src=\"" + image+ "\">", null, "text/html"
            );
            message.AlternateViews.Add(confEmail);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("jtn2dsng", "Tw!$3r@1"),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

    }
}
