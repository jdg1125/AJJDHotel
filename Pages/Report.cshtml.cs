using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AJJDHotel.Models;
using AJJDHotel.Utility;

namespace AJJDHotel.Pages
{
   [Authorize(Roles = SD.AdminUser)]
    public class ReportModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
