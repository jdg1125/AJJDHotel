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

        [BindProperty, TempData]
        public DateTime StartDate { get; set; }

        [BindProperty, TempData]
        public DateTime EndDate { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;




        public SearchResultsModel(IDbAccess dbAccess, UserManager<ApplicationUser> userManager)
        {
            this.dbAccess = dbAccess;
            AvailableRoomTypes = new List<RoomType>();

            StartDate = new DateTime();
            EndDate = new DateTime();

            _userManager = userManager;

        }

        public void OnGet(DateTime start, DateTime end)
        {

            StartDate = start;
            EndDate = end;

            if (StartDate < EndDate)
            {
                AvailableRoomTypes = dbAccess.GetAvailableRoomTypes(StartDate, EndDate);
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
            if (User.Identity.IsAuthenticated)
            {
                if (TempData.Peek("StartDate") != null)
                {
                    StartDate = (DateTime)TempData.Peek("StartDate");
                    EndDate = (DateTime)TempData.Peek("EndDate");
                }
                else
                {
                    StartDate = (DateTime)TempData.Peek("checkin");
                    EndDate = (DateTime)TempData.Peek("checkout");
                }
                return RedirectToPage("OrderSummary", new { start = StartDate, end = EndDate, id = id });
            }
            else
            {
                /// very important comments
                var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");
                //C:\Users\student\Documents\GitHub\AJJDHotel\Areas\Identity\Pages\Account\Register.cshtml
                return RedirectToPage("/Account/Register", new { Area = "Identity", returnUrl = $"~/SearchResults/{Request.QueryString}" });
                // var location = new Uri($"{Request.Scheme}://{Request.Host}");
                //return RedirectToPage( location+ "/Identity/Account/Register");
                //return Redirect("~/Identity/Account/Register", );
                //var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");
                //string url = new Uri($"{Request.Scheme}://{Request.Host}") + "Identity/Account/Register";//+ Request.Query[location.ToString()];
                //return RedirectToPage(url); // , new { returnUrl = location});
                //return Redirect(url);

                //var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");

                //var url = location.AbsoluteUri;
                //return RedirectToPage("~/Identity/Account/Register", new { returnUrl = url });

            }
        }


    }
}