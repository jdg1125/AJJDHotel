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
        Reservation GetReservationByConfirmationNumber(string confirmationNumber);
        List<Reservation> GetReservationsByName(string name);
    }
}
