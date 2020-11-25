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
        List<Reservation> GetReservationsByName(string name);
        Room GetAvailableRoomByRoomTypeId(int roomTypeId, DateTime startDate, DateTime endDate);
        RoomType GetRoomTypeByRoomTypeId(int roomTypeId);
        ApplicationUser GetUserById(string userId);
    }
}
