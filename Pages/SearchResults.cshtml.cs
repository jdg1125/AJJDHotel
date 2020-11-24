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
using AJJDHotel.Data;

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
            return RedirectToPage("Purchase", new { start = tempStartDate, end = tempEndDate,id= id });
        }


    }
}