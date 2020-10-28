using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AJJDHotel.Pages
{
    // TODO remove all this crap
    public class OrderConfirmationModel : PageModel
    {
        public List<RoomType> Results { get; set; }
        public void OnGet()
        {
            Results = new List<RoomType>(3);

            Results.Add(new RoomType()
            {
                RoomName = "Standard Room",
                Rate = 150,
                Description = "The standard room offers beautiful views of the restaurant's loading deck and the staff parking lot. Our widely talked about super thin walls allow you to get to know your neighbors. Don't expect it to look anything like the picture.",
                ImgPath = "https://www.bakuun.com/img/rooms/r1.jpg"
            });

            Results.Add(new RoomType()
            {
                RoomName = "Junior Suite",
                Rate = 250,
                Description = "Exactly like the standard room, but we use newer sheets and towels. You're paying for the ability to say that you stayed in a suite.  Don't expect it to look anything like the picture.",
                ImgPath = "https://www.bakuun.com/img/rooms/r1.jpg"
            });

            Results.Add(new RoomType()
            {
                RoomName = "Deluxe Suite",
                Rate = 350,
                Description = "Exquisitely appointed, our nicest room has recently been renovated following the horrific event that gave it its reputation.  Don't expect it to look anything like the picture.",
                ImgPath = "https://hi-cdn.t-rp.co.uk/images/hotels/2746051/38?width=870&height=480&crop=false"
            });

        }
    }
}
