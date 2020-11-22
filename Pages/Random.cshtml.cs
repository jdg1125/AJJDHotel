using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AJJDHotel.Data;
using AJJDHotel.Models;

namespace AJJDHotel.Pages
{
    public class RandomModel : PageModel
    {
        private readonly AJJDHotel.Data.ApplicationDbContext _context;

        public RandomModel(AJJDHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RoomType RoomType { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RoomTypes.Add(RoomType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
