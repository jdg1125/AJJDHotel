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
                .FirstOrDefault();
        }

        public List<Room> GetAllAvailableRoomsByRoomType(int roomTypeId, DateTime startDate, DateTime endDate)
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

            List<Room> results = new List<Room>();
            results.AddRange(context.Rooms
                .FromSqlRaw(getAvailableRoomsSql, startDateP, endDateP)
                .Where(x => x.RoomTypeId == roomTypeId));
            return results;
        }

        public List<string> GetDistinctBeds()
        {
                return context.RoomTypes
                .Select(x => x.Beds)
                .Distinct().ToList();
            
        }

        public List<string> GetDistinctViews()
        {
            return context.RoomTypes
            .Select(x => x.View)
            .Distinct().ToList();

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

        public ApplicationUser GetUserByEmail(string email)
        {
            return context.ApplicationUsers.FirstOrDefault(user => user.Email == email);
        }

        public Reservation GetReservationByConfirmationNumber(int confirmationNumber)
        {
            if (confirmationNumber >= 8744305)
            {
                int reservationId = confirmationNumber - 8744304;

                return GetReservationByReservationId(reservationId);
            }
            else
            {
                // TODO do better
                throw new Exception();
            }
        }

        public Reservation GetReservationByReservationId(int reservationId)
        {
            return context.Reservations
                  .Find(reservationId);
        }

     /*   public List<Reservation> GetReservationsByUserId(string id)
        {
            return context.Reservations
                .Where(x => x.Id == id)
                .ToList();
        }*/

        public List<RoomType> GetRoomTypes()
        {
            return context.RoomTypes
                .OrderBy(x => x.Beds)
                .OrderBy(x => x.View)
                .OrderByDescending(x => x.RoomName)
                .ToList();
        }

        public void DeleteReservation(int reservationId)
        {
            var reservation = GetReservationByReservationId(reservationId);
            context.Reservations.Remove(reservation);
            var affectedRecords = context.SaveChanges();
        }

        //public RoomType GetRoomByReservationId(int reservationId)
        //{
        //    return context.Reservations
        //        .Where(res => res.ReservationId == reservationId)
        //        .Include(r => r.RoomId)
        //            .ThenInclude(rt => rt.)
        //}

        public void UpdateRoomType(int roomtypeId, string description, string beds, string view, string roomname, decimal rate, string imgpath)
        {
            var room = GetRoomTypeByRoomTypeId(roomtypeId);
            context.SaveChanges();  //redundant?
            room.Description = description;
            room.Beds = beds;
            room.View = view;
            room.RoomName = roomname;
            room.Rate = rate;
            room.ImgPath = imgpath;
            var affectedRecords = context.SaveChanges();
        }


        public List<Reservation> GetReservationsByUserId(string userId)
        {
            List<Reservation> result = new List<Reservation>();

            result.AddRange(context.Reservations
                            .Include(res => res.Room)
                            .ThenInclude(room => room.RoomType)
                            .Where(res => res.Id == userId));

            return result;
        }

        public Reservation GetReservationByConfirmation(int num)
        {
            Reservation result = null;
            if (num >= 8744305)
            {
                num = num - 8744304;

                var query = context.Reservations
                       .Include(res => res.Room)
                       .ThenInclude(room => room.RoomType)
                       .Where(res => res.ReservationId == num);

                result = query.FirstOrDefault();
            }

            return result;
        }

        public void UpdateApplicationUser(ApplicationUser user)
        {
            var affectedRecords = context.SaveChanges();
        }



    }
}
