using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AJJDHotel.Models;
using AJJDHotel.Data;

namespace AJJDHotel.Pages
{
    public class SearchResultsModel : PageModel
    {
        public List<RoomType> Results { get; set; }
        public ApplicationDbContext context;
        //public IQueryable query; 

        public void OnGet()
        {
            var query = context.RoomTypes;
            this.Results.AddRange(query);
                                
        }

        public SearchResultsModel(ApplicationDbContext context)
        {
            this.context = context;
            Results = new List<RoomType>();
        }

     
    }
}
