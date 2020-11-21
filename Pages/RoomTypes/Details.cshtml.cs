using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AJJDHotel.Data;
using AJJDHotel.Models;

namespace AJJDHotel.Pages.RoomTypes
{
    public class DetailsModel : PageModel
    {
        private readonly AJJDHotel.Data.ApplicationDbContext _context;

        public DetailsModel(AJJDHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public RoomType RoomType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomType = await _context.RoomTypes.FirstOrDefaultAsync(m => m.RoomTypeId == id);

            if (RoomType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
