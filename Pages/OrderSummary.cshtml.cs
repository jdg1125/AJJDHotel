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
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;

namespace AJJDHotel.Pages
{
    [Authorize]
    public class OrderSummaryModel : PageModel
    {
        private readonly IDbAccess dbAccess;
        public readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<OrderSummaryModel> _logger;

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

        [BindProperty]
        public InputModel Input { get; set; }

        public OrderSummaryModel(IDbAccess dbAccess, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<OrderSummaryModel> logger)

        {
            this.dbAccess = dbAccess;
            UserManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            this.RoomType = new RoomType();
            this.Room = new Room();
            StartDate = new DateTime();
            EndDate = new DateTime();

        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            public string Password { get; set; }

            [Required]
            [Display(Name = "Birthday")]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; }

            [Required]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }


        }

        public IActionResult OnGet(int id)
        {
            try
            {
                NumGuests = (int)TempData.Peek("numGuests");
                StartDate = (DateTime)TempData.Peek("checkin");
                EndDate = (DateTime)TempData.Peek("checkout");

                RoomTypeId = id;

                Id = UserManager.GetUserId(User);
                ApplicationUser = dbAccess.GetUserById(Id);

                double nights = (EndDate - StartDate).TotalDays;

                // get room type to calculate total charge for reservation
                // TODO do these 2 queries in one query
                RoomType = dbAccess.GetRoomTypeByRoomTypeId(id);

                decimal tax = 0.08M;
                TotalCharge = (decimal)nights * RoomType.Rate * (1 + tax); ;

                // gets first available room that has the desired room type id (need this to get room id for CreateReservation)
                Room = dbAccess.GetAvailableRoomByRoomTypeId(id, StartDate, EndDate);

                return Page();
            }
            catch (NullReferenceException e)
            {
                return RedirectToPage("Sorry");
            }
        }

        public async Task<IActionResult> OnPost(DateTime startdate, DateTime enddate, int numguests, int roomid, decimal totalcharge, string id, int roomtypeid)
        {
            DateTime now = DateTime.Now;
            int age = new DateTime(DateTime.Now.Subtract(Input.DOB).Ticks).Year - 1;
            if (age < 18) return Page();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    DOB = Input.DOB,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };

                Input.Password = GeneratePassword();
                var result = await UserManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    AJJDEmailRegister();
                    await UserManager.AddToRoleAsync(user, SD.CustomerUser);
                    _logger.LogInformation("User created a new account with password.");
                }

                id = dbAccess.GetUserByEmail(Input.Email).Id; //Because Admin is logged in, we overwrite id regardless of whether a new account was created.
            }

            // CreateReservation method returns the PK of the newly-created Reservation; use it to get confirmation number
            int resId = dbAccess.CreateReservation(startdate, enddate, numguests, roomid, totalcharge, id);

            return RedirectToPage("/OrderConfirmation", new { reservationId = resId, roomTypeId = roomtypeid , password = Input.Password});
        }

        // possible helper method
        public decimal GetTotalCharge(int roomTypeId)
        {
            throw new NotImplementedException();
        }

        public void AJJDEmailRegister()
        {
            MailAddress to = new MailAddress(Input.Email);
            MailAddress from = new MailAddress("jtn2dsng@gmail.com");

            MailMessage message = new MailMessage(from, to);
            message.Subject = "AJJD Hotel Registration";
            message.Body = "Thank you for registering at AJJD Hotel";

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

        public static string GeneratePassword(int passwordSize = 8)
        {
            const string LOWER = "abcdefghijklmnopqrstuvwxyz";
            const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMBER = "0123456789";
            const string SPECIAL = "!@#$%^&*()?";



            string allowed = LOWER + UPPER + NUMBER + SPECIAL;

            Random _random = new Random();
            string password = "";
            string[] requiredTypes = new string[] { LOWER, UPPER, NUMBER, SPECIAL };

            string[] passwordArray = new string[passwordSize];
            List<int> availableSlots = new List<int>();
            for (int i = 0; i < passwordSize; i++)
            {
                availableSlots.Add(i);
            }
            foreach (string requiredType in requiredTypes)
            {

                int nextSlotIndex = _random.Next(availableSlots.Count);
                int nextSlot = availableSlots.ElementAt(nextSlotIndex);
                string nextChar = requiredType[_random.Next(requiredType.Length)].ToString();
                passwordArray[nextSlot] = nextChar;
                availableSlots.Remove(nextSlot);
            }
            for (int i = 0; i < passwordArray.Length; i++)
            {
                if (passwordArray[i] == null)
                {
                    passwordArray[i] = allowed[_random.Next(allowed.Length)].ToString();
                }
            }

            foreach (var a in passwordArray)
            {
                password += a;
            }
            return password;
        }




    }
}
