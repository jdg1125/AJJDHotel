using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AJJDHotel.Models;
using AJJDHotel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AJJDHotel.Utility;

namespace AJJDHotel.Pages
{
    [Authorize(Roles = SD.AdminUser)]

    public class ReportModel : PageModel
    {

        public List<Reservation> SRCount { get; set; }
        public List<Reservation> JSCount { get; set; }
        public List<Reservation> DSCount { get; set; }
        public int SROcc { get; set; }
        public int JSOcc { get; set; }
        public int DSOcc { get; set; }
        public ApplicationDbContext context;

        public void OnGet()
        {

        }

        public void OnPost(DateTime checkin_date, DateTime checkout_date)
        {
            var getRev = context.Reservations
                                .Where(r => (r.Room.RoomType.RoomName.Equals("Standard Room")) && (r.StartDate.CompareTo(checkin_date) >= 0) && (r.EndDate.CompareTo(checkout_date) <= 0));
            this.SRCount.AddRange(getRev);

            var getJS = context.Reservations
                                .Where(r => (r.Room.RoomType.RoomName.Equals("Junior Suite")) && (r.StartDate.CompareTo(checkin_date) >= 0) && (r.EndDate.CompareTo(checkout_date) <= 0));
            this.JSCount.AddRange(getJS);

            var getDS = context.Reservations
                                .Where(r => (r.Room.RoomType.RoomName.Equals("Deluxe Suite")) && (r.StartDate.CompareTo(checkin_date) >= 0) && (r.EndDate.CompareTo(checkout_date) <= 0));
            this.DSCount.AddRange(getDS);



            var getSROcc = context.Reservations
                                  .Where(r => (r.Room.RoomType.RoomName.Equals("Standard Room")) && (r.StartDate.CompareTo(checkin_date) >= 0) && (r.EndDate.CompareTo(checkout_date) <= 0))
                                  .Count();
            SROcc = getSROcc;

            var getJSOcc = context.Reservations
                                  .Where(r => (r.Room.RoomType.RoomName.Equals("Junior Suite")) && (r.StartDate.CompareTo(checkin_date) >= 0) && (r.EndDate.CompareTo(checkout_date) <= 0))
                                  .Count();
            JSOcc = getJSOcc;

            var getDSOcc = context.Reservations
                                  .Where(r => (r.Room.RoomType.RoomName.Equals("Deluxe Suite")) && (r.StartDate.CompareTo(checkin_date) >= 0) && (r.EndDate.CompareTo(checkout_date) <= 0))
                                  .Count();
            DSOcc = getDSOcc;

        }

        public ReportModel(ApplicationDbContext context)
        {
            this.context = context;
            SRCount = new List<Reservation>();
            JSCount = new List<Reservation>();
            DSCount = new List<Reservation>();
        }
    }
}

