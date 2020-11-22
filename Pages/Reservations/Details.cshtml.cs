using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AJJDHotel.Data;
using AJJDHotel.Models;

namespace AJJDHotel.Pages.Reservations
{
    public class DetailsModel : PageModel
    {
        private readonly AJJDHotel.Data.ApplicationDbContext _context;

        public DetailsModel(AJJDHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
    }
}
