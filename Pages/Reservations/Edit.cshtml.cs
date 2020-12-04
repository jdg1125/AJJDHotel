using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AJJDHotel.Data;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Authorization;
using AJJDHotel.Utility;

namespace AJJDHotel.Pages.Reservations
{
    [Authorize(Roles = SD.AdminUser)]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbAccess _dbAccess;

        public EditModel(ApplicationDbContext context, IDbAccess dbAccess)
        {
            _context = context;
            _dbAccess = dbAccess;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public string ResNumber { get; set; }
        public string GuestEmail { get; set; }
        public int numDays { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string resNumber, string guestEmail)
        {
            if (id == null)
            {
                return NotFound();
            }

            ResNumber = resNumber;
            GuestEmail = guestEmail;

            Reservation = await _context.Reservations
                .Include(r => r.ApplicationUser)
                .Include(r => r.Room)
                .ThenInclude(room => room.RoomType).FirstOrDefaultAsync(m => m.ReservationId == id);

            if (Reservation == null)
            {
                return NotFound();
            }

            List<Room> AvailableRooms = _dbAccess.GetAllAvailableRoomsByRoomType(Reservation.Room.RoomType.RoomTypeId, 
                Reservation.StartDate, Reservation.EndDate);
            AvailableRooms.Add(Reservation.Room);

           ViewData["RoomId"] = new SelectList(AvailableRooms, nameof(Room.RoomId), nameof(Room.RoomNumber));

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string resNumber, string guestEmail, int numDays)
        {
            if (!ModelState.IsValid || Reservation.EndDate <= Reservation.StartDate)
            {
                return RedirectToPage("./Edit", new { id = Reservation.ReservationId, guestEmail=guestEmail, numDays=numDays});
            }
            ReduceTotalCharge(numDays);
            _context.Attach(Reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(Reservation.ReservationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../ManageReservations", new {resNumber=resNumber, guestEmail = guestEmail });
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }

        public void ReduceTotalCharge(int numDays)
        {
            int new_numDays = (Reservation.EndDate - Reservation.StartDate).Days;
            Reservation.TotalCharge *= ((decimal)new_numDays / numDays);
        }
    }
}
