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
    public class IndexModel : PageModel
    {
        private readonly AJJDHotel.Data.ApplicationDbContext _context;

        public IndexModel(AJJDHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get;set; }

        public async Task OnGetAsync()
        {
            Reservation = await _context.Reservations
                .Include(r => r.ApplicationUser)
                .Include(r => r.Room).ToListAsync();
        }
    }
}
