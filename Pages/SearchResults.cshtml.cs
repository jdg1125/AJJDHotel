using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AJJDHotel.Models;
using AJJDHotel.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace AJJDHotel.Pages
{
    public class SearchResultsModel : PageModel
    {
        public List<RoomType> AvailableRoomTypes { get; set; }

        private readonly IDbAccess dbAccess;

        public DateTime tempStartDate;
        public DateTime tempEndDate;


        public SearchResultsModel(IDbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
            AvailableRoomTypes = new List<RoomType>();
            tempStartDate = new DateTime();
            tempEndDate = new DateTime();

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

        }
        
        public IActionResult OnGetReserve(int id)
        {
         
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("OrderSummary", new { start = tempStartDate, end = tempEndDate, id = id});
            }
            else
            {
                /// very important comments
                var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");
                //C:\Users\student\Documents\GitHub\AJJDHotel\Areas\Identity\Pages\Account\Register.cshtml
                return RedirectToPage("/Account/Register", new { Area = "Identity", returnUrl = $"~/SearchResults/{Request.QueryString}" } );
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