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

namespace AJJDHotel.Pages.Rooms
{
    public class EditModel : PageModel
    {
        private readonly AJJDHotel.Data.ApplicationDbContext _context;

        public EditModel(AJJDHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Room Room { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms
                .Include(r => r.RoomType).FirstOrDefaultAsync(m => m.RoomId == id);

            if (Room == null)
            {
                return NotFound();
            }
           ViewData["RoomTypeId"] = new SelectList(_context.RoomTypes, "RoomTypeId", "RoomTypeId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(Room.RoomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
