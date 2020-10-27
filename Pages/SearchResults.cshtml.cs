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
            Results = new List<ResultItem>(3);

            Results.Add(new ResultItem()
            {
                Name = "Standard room",
                Rate = 150,
                Description = "All of our standard rooms offer beautiful views of the restaurant's loading deck and the staff parking lot. Our widely talked about super thin walls allow you to get to know your neighbors. Don't expect it to look anything like the picture.",
                imgPath = "https://www.bakuun.com/img/rooms/r1.jpg"
            });

            Results.Add(new ResultItem()
            {
                Name = "Junior Suite",
                Rate = 250,
                Description = "Exactly like the standard room, but we use newer sheets and towels. You're paying for the ability to say that you stayed in a suite.  Don't expect it to look anything like the picture.",
                imgPath = "https://www.bakuun.com/img/rooms/r1.jpg"
            });

            Results.Add(new ResultItem()
            {
                Name = "Deluxe Suite",
                Rate = 350,
                Description = "Exquisitely appointed, our nicest room has recently been renovated following the horrific event that gave it its reputation.  Don't expect it to look anything like the picture.",
                imgPath = "https://hi-cdn.t-rp.co.uk/images/hotels/2746051/38?width=870&height=480&crop=false"
            });

        }
    }
}
