//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using AJJDHotel.Models;

//namespace AJJDHotel.Pages
//{
//    public class ManageReservationsModel : PageModel
//    {
//        public List<Reservation> QueryByName { get; set; }
//        public Reservation QueryByConfNum { get; set; }
//        public void OnGet()
//        {
//            QueryByName = new List<Reservation>();
//            QueryByConfNum = new Reservation()
//            { 
//                ConfirmationNum = "A46C938JF1",
//                RoomNum = "E1202",
//                Location = "Augusta, GA",
//                Checkin = new DateTime(2021, 6, 12),
//                Checkout = new DateTime(2021, 6, 19),
//                TotalCharge = 932.76, 
//                NameFirstLast = "John Smith",
//                RoomType = "Junior Suite"
//            };

//            QueryByName.Add(QueryByConfNum);

//            QueryByName.Add(new Reservation()
//            {
//                ConfirmationNum = "B7320AQ133",
//                RoomNum = "E1018",
//                Location = "Augusta, GA",
//                Checkin = new DateTime(2021, 11, 20),
//                Checkout = new DateTime(2021, 11, 27),
//                TotalCharge = 788.02,
//                NameFirstLast = "John Smith",
//                RoomType = "Standard Room"
//            });

//        }
//    }
//}
