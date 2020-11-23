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

        public DateTime tempStartDate { get; set; }
        public DateTime tempEndDate { get; set; }


        public void OnGet()
        {
            //var query = context.RoomTypes;
            //this.Results.AddRange(query);

            tempStartDate = new DateTime(2020, 12, 7);
            tempEndDate = new DateTime(2020, 12, 9);

            //var roomsQuery = from r in context.Rooms select r;
            //var roomsSortedByRoomTypeQuery = roomsQuery.OrderBy(x => x.RoomType);
            //var roomsSortedByRoomTypeResults = roomsSortedByRoomTypeQuery.ToList();
            //var reservationsQuery = from res in context.Reservations select res;
            //var reservationsWithStartDateAfterTodayQuery = reservationsQuery
            //    .Where(x => x.StartDate > DateTime.Now);
            //var reservationsWithStartDateAfterTodayResults = reservationsWithStartDateAfterTodayQuery.ToList();
            //var roomQuery = from r in context.Rooms select r;
            //var roomSelected = context.Rooms.Find(2);
            //var roomSelected2 = context.Rooms.Single(x => x.RoomNumber == 109);
            //var roomTypesList = context.RoomTypes
            //    .Where(x => x.RoomName.Contains("Suite") && x.Beds.Contains("2")).ToList();
            //var applicationUsersQuery = from appusers in context.ApplicationUsers select appusers;
            // var applicationUsersQuery = from appusers in context.ApplicationUsers select appusers;

            var roomFromSql = context.Rooms
                .FromSqlRaw("SELECT * " +
                "FROM Rooms WHERE RoomNumber = @p0", "109")
                .SingleOrDefault();

            var newSql = @"SELECT Rooms.RoomId
            FROM Rooms
            INNER JOIN RoomTypes ON Rooms.RoomTypeId = RoomTypes.RoomTypeId
            WHERE Rooms.RoomId NOT IN(SELECT Rooms.RoomId
                                      FROM Rooms t1
                                      INNER JOIN Reservations t2 ON t1.RoomId = t2.RoomId
                                      WHERE ([tempStartDate] > t2.StartDate)";

            var myRawSqlForGetAvailableRooms = @"SELECT DISTINCT RoomTypes.RoomTypeId, Description, Beds, View, RoomName, Rate, ImgPath
                                                            FROM RoomTypes
                                                            INNER JOIN Rooms ON Rooms.RoomTypeId = RoomTypes.RoomTypeId
                                                            WHERE Rooms.RoomId NOT IN(SELECT Rooms.RoomId
                                                                                      FROM Rooms
                                                                                      INNER JOIN Reservations ON Rooms.RoomId = Reservations.RoomId
                                                                                      WHERE ('2020-12-07 00:00:00' > Reservations.StartDate))";


            var unavailableRoomsAttempt2 = context.Reservations
                .Where(res => res.StartDate <= tempStartDate)
                .ToList();

            this.AvailableRoomTypes = context.RoomTypes
                .FromSqlRaw(myRawSqlForGetAvailableRooms)
                .ToList();

            string rawSqlQuery =
                @"SELECT RoomId FROM Reservations
                            WHERE (StartDate <= @p0)";

            var unavailableRooms = context.Rooms
                .FromSqlRaw(rawSqlQuery, tempStartDate);

            var unavailableRooms3 = context.Reservations
                .FromSqlRaw($"SELECT RoomId FROM Reservations WHERE StartDate <= {tempStartDate}");


            var query = context.RoomTypes;


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