using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AJJDHotel.Models;
using AJJDHotel.Data;

namespace AJJDHotel.Pages
{
    public class SearchResultsModel : PageModel
    {
        public List<RoomType> AvailableRoomTypes { get; set; }
        public List<RoomType> Results { get; set; }
        public ApplicationDbContext context;
        //public IQueryable query; 

        public string tempStartDate { get; set; }
        public string tempEndDate { get; set; }


        public void OnGet()
        {
          

            tempStartDate = "2020-12-07 00:00:00";
            tempEndDate = "2020-12-10 00:00:00";

            var myRawSqlForGetAvailableRooms = 
                @$"SELECT DISTINCT RoomTypes.RoomTypeId, Description, Beds, View, RoomName, Rate, ImgPath
                  FROM RoomTypes
                  INNER JOIN Rooms ON Rooms.RoomTypeId = RoomTypes.RoomTypeId
                  WHERE Rooms.RoomId NOT IN(SELECT Rooms.RoomId
                                            FROM Rooms
                                            INNER JOIN Reservations ON Rooms.RoomId = Reservations.RoomId
                                            WHERE (Reservations.StartDate <= '2020-12-07 00:00:00' and Reservations.EndDate > '2020-12-07 00:00:00')
                                                or (Reservations.StartDate < '2020-12-10 00:00:00' and Reservations.EndDate > '2020-12-10 00:00:00')
                                                or (Reservations.StartDate >= '2020-12-07 00:00:00' and Reservations.EndDate < '2020-12-10 00:00:00'))";

           
            this.AvailableRoomTypes = context.RoomTypes
                .FromSqlRaw(myRawSqlForGetAvailableRooms)
                .ToList();

        }

        public SearchResultsModel(ApplicationDbContext context)
        {
            this.context = context;
            AvailableRoomTypes = new List<RoomType>();
            //tempStartDate = new DateTime();
            //tempEndDate = new DateTime();

        }
    }
}