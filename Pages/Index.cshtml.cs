using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AJJDHotel.Models;

namespace AJJDHotel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }

        [BindProperty, TempData]
        public DateTime checkin { get; set; }
        [BindProperty, TempData]
        public DateTime checkout { get; set; }

        public IndexModel(ILogger<IndexModel> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            SignInManager = signInManager;
            _logger = logger;
            UserManager = userManager;
        }

        public void OnGet()
        { 
            
        }
        public IActionResult OnPost()
        {

            return RedirectToPage("SearchResults", new { start = checkin, end = checkout });
        }

    }
    }

