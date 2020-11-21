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
    public class DeleteModel : PageModel
    {
        private readonly AJJDHotel.Data.ApplicationDbContext _context;

        public DeleteModel(AJJDHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomType = await _context.RoomTypes.FindAsync(id);

            if (RoomType != null)
            {
                _context.RoomTypes.Remove(RoomType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
