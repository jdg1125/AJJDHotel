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
        public List<RoomType> Results { get; set; }
        public bool IsSentFromNav { get; set; }
        public void OnGet(bool sorry)
        {
            IsSentFromNav = sorry;
            Results = new List<RoomType>(3);
            AddDummyResults();
        }

        public SearchResultsModel()
        {
            IsSentFromNav = false;
            Results = new List<RoomType>(3);
            AddDummyResults();
        }

        private void AddDummyResults()
        {
            Results.Add(new RoomType()
            {
                RoomName = "Standard Room",
                Rate = 150,
                Description = "The Standard Room offers beautiful views of the restaurant's loading deck and the staff parking lot. Our widely talked about super thin walls allow you to get to know your neighbors. Don't expect it to look anything like the picture.",
                ImgPath = "https://www.bakuun.com/img/rooms/r1.jpg",
                View = "Parking lot/loading dock",
                Beds = "1 Queen"
            });

            Results.Add(new RoomType()
            {
                RoomName = "Junior Suite",
                Rate = 250,
                Description = "Exactly like the Standard Room, but we use newer sheets and towels. You're paying for the ability to say that you stayed in a suite.  Don't expect it to look anything like the picture.",
                ImgPath = "https://www.bakuun.com/img/rooms/r2.jpg",
                View = "Partial River",
                Beds = "2 Queen"
            });

            if (!IsSentFromNav)
                Results.Add(new RoomType()
                {
                    RoomName = "Deluxe Suite",
                    Rate = 350,
                    Description = "Exquisitely appointed, our nicest room has recently been renovated following the horrific event that gave it its reputation.  Don't expect it to look anything like the picture.",
                    ImgPath = "https://www.bakuun.com/img/rooms/r3.jpg",
                    View = "River",
                    Beds = "2 King"
                });
        }
    }
}
