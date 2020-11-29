using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AJJDHotel.Data;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Authorization;
using AJJDHotel.Utility;

namespace AJJDHotel.Pages.Reservations
{
    [Authorize(Roles=SD.AdminUser)]
    public class DeleteModel : PageModel
    {
        private readonly AJJDHotel.Data.ApplicationDbContext _context;

        public DeleteModel(AJJDHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public string ResNumber { get; set; }
        public string GuestEmail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string resNumber, string guestEmail)
        {
            ResNumber = resNumber;
            GuestEmail = guestEmail;

            if (id == null)
            {
                return NotFound();
            }

            Reservation = await _context.Reservations
                .Include(r => r.ApplicationUser)
                .Include(r => r.Room).FirstOrDefaultAsync(m => m.ReservationId == id);

            if (Reservation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string resNumber, string guestEmail)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation = await _context.Reservations.FindAsync(id);

            if (Reservation != null)
            {
                _context.Reservations.Remove(Reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../ManageReservations", new { resNumber=resNumber, guestEmail=guestEmail});
        }
    }
}
