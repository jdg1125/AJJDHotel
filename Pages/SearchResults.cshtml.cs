using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AJJDHotel.Models;

namespace AJJDHotel.Pages
{
    public class SearchResultsModel : PageModel
    {
        public List<ResultItem> Results { get; set; }
        public void OnGet()
        {
            Results = new List<ResultItem>(12);

            for (int i = 0; i < 12; i++)
                Results.Add(new ResultItem()
                {
                    Name = "Standard room",
                    Rate = 150,
                    Description = "All of our standard rooms offer beautiful views of the restaurant's loading deck and the staff parking lot. Our widely talked about super thin walls allow you to get to know your neighbors. Don't expect it to look anything like the picture.",
                    imgPath = "https://www.bakuun.com/img/rooms/r1.jpg"
                });
        }
    }
}
