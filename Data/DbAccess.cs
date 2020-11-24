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

            string getAvailableRoomTypesSql =
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
                    .FromSqlRaw(getAvailableRoomTypesSql, startDateP, endDateP)
                    .ToList();

        }


        public int CreateReservation(DateTime startDate, DateTime endDate, int numGuests, int roomId, decimal totalCharge, string userId)
        {
            Reservation myRes = new Reservation { StartDate = startDate, EndDate = endDate, NumGuests = numGuests, RoomId = roomId, TotalCharge = totalCharge };
            context.Reservations
                .Add(myRes);
            var affectedRecords = context.SaveChanges();
            // returns the PK of the reservation that was just made
            return myRes.ReservationId;

        }


        // TODO these two queries should be one that return id and rate
        public Room GetAvailableRoomByRoomTypeId(int roomTypeId, DateTime startDate, DateTime endDate)
        {
            SqliteParameter startDateP = new SqliteParameter("@startDate", startDate);
            SqliteParameter endDateP = new SqliteParameter("@endDate", endDate);

            // get bad rooms
            string getAvailableRoomsSql = @"SELECT Rooms.RoomId, RoomNumber, Rooms.RoomTypeId
                                            FROM Rooms
                                            INNER JOIN RoomTypes ON RoomTypes.RoomTypeId = Rooms.RoomTypeId
                                            WHERE Rooms.RoomId NOT IN (
	                                            SELECT Rooms.RoomId
	                                            FROM Rooms
	                                            INNER JOIN Reservations ON Rooms.RoomId = Reservations.RoomId
	                                            WHERE ((Reservations.StartDate <= '2020-12-06 00:00:00' and Reservations.EndDate > '2020-12-06 00:00:00')
	                                                or (Reservations.StartDate < '2020-12-10 00:00:00' and Reservations.EndDate > '2020-12-10 00:00:00')
	                                                or (Reservations.StartDate >= '2020-12-06 00:00:00' and Reservations.EndDate < '2020-12-10 00:00:00')))";

            return context.Rooms
                .FromSqlRaw(getAvailableRoomsSql, startDateP, endDateP)
                .Where(x => x.RoomTypeId == roomTypeId)
                .FirstOrDefault();
        }


        // TODO gave up on getting specific element from table (room rate), but we do not need entire entity
        public RoomType GetRoomTypeByRoomTypeId(int roomTypeId)
        {
            return context.RoomTypes
                .Where(x => x.RoomTypeId == roomTypeId)
                .FirstOrDefault();

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
