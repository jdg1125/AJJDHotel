using AJJDHotel.Models;
using AJJDHotel.Pages;
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
            Reservation myRes = new Reservation { StartDate = startDate, EndDate = endDate, NumGuests = numGuests, RoomId = roomId, TotalCharge = totalCharge, Id = userId };
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
	                                            WHERE ((Reservations.StartDate <= @startDate and Reservations.EndDate > @startDate)
	                                                or (Reservations.StartDate < @endDate and Reservations.EndDate > @endDate)
	                                                or (Reservations.StartDate >= @startDate and Reservations.EndDate < @endDate)))";

            return context.Rooms
                .FromSqlRaw(getAvailableRoomsSql, startDateP, endDateP)
                .Where(x => x.RoomTypeId == roomTypeId)
                .First();
        }


        // TODO gave up on getting specific element from table (room rate), but we do not need entire entity
        public RoomType GetRoomTypeByRoomTypeId(int roomTypeId)
        {
            return context.RoomTypes
                .Where(x => x.RoomTypeId == roomTypeId)
                .FirstOrDefault();

        }

        public ApplicationUser GetUserById(string userId)
        {
            return context.ApplicationUsers
                .Find(userId);
        }


        public Reservation GetReservationByConfirmationNumber(int confirmationNumber)
        {
            if (confirmationNumber >= 8744305)
            {
                int pk = confirmationNumber - 8744304;

                return context.Reservations
                    .Find(pk);
            }
            else
            {
                // TODO do better
                throw new Exception();
            }
        }


        public List<Reservation> GetReservationsByUserId(string id)
        {
            return context.Reservations
                .Where(x => x.Id == id)
                .ToList();
        }

        public List<RoomType> GetRoomTypes()
        {
            return context.RoomTypes
                .ToList();
        }

        //public RoomType GetRoomByReservationId(int reservationId)
        //{
        //    return context.Reservations
        //        .Where(res => res.ReservationId == reservationId)
        //        .Include(r => r.RoomId)
        //            .ThenInclude(rt => rt.)
        //}


        public List<Reservation> GetReservationsByName(string name)
        {
            throw new NotImplementedException();
        }


    }
}
