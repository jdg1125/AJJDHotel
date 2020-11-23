using AJJDHotel.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AJJDHotel.Data
{
    public class DbAccess : IDbAccess
    {
        private readonly ApplicationDbContext context;

        public DbAccess(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<RoomType> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            SqliteParameter startDateP = new SqliteParameter("@startDate", startDate);
            SqliteParameter endDateP = new SqliteParameter("@endDate", endDate);

            string getAvailableRoomsSql =
                @"SELECT DISTINCT RoomTypes.RoomTypeId, Description, Beds, View, RoomName, Rate, ImgPath
                  FROM RoomTypes
                  INNER JOIN Rooms ON Rooms.RoomTypeId = RoomTypes.RoomTypeId
                  WHERE Rooms.RoomId NOT IN(SELECT Rooms.RoomId
                                            FROM Rooms
                                            INNER JOIN Reservations ON Rooms.RoomId = Reservations.RoomId
                                            WHERE ((Reservations.StartDate <= @startDate and Reservations.EndDate > @startDate)
                                                or (Reservations.StartDate < @endDate and Reservations.EndDate > @endDate)
                                                or (Reservations.StartDate >= @startDate and Reservations.EndDate < @endDate)))";

            return context.RoomTypes
                    .FromSqlRaw(getAvailableRoomsSql, startDateP, endDateP)
                    .ToList();

        }


        public Reservation GetReservationByConfirmationNumber(string confirmationNumber)
        {
            throw new NotImplementedException();
        }


        public List<Reservation> GetReservationsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
