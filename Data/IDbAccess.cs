using AJJDHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AJJDHotel.Data
{
    public interface IDbAccess
    {
        List<RoomType> GetAvailableRoomTypes(DateTime startDate, DateTime endDate);
        int CreateReservation(DateTime startDate, DateTime endDate, int numGuests, int roomId, decimal totalCharge, string userId);
        Reservation GetReservationByConfirmationNumber(int confirmationNumber);
        Reservation GetReservationByReservationId(int reservationId);
        List<Reservation> GetReservationsByName(string name);
        List<Reservation> GetReservationsByUserId(string id);
        Room GetAvailableRoomByRoomTypeId(int roomTypeId, DateTime startDate, DateTime endDate);
        //Room GetRoomByReservationId(int reservationId);
        List<RoomType> GetRoomTypes();
        RoomType GetRoomTypeByRoomTypeId(int roomTypeId);
        ApplicationUser GetUserById(string userId);
        void DeleteReservation(int reservationId);
        void UpdateRoomType(int roomtypeId, string description, string beds, string view, string roomname, decimal rate, string imgpath);
        //List<RoomType> GetDisctinctBedTypes();
    }
}
