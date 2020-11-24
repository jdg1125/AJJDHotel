using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AJJDHotel.Pages
{
    public class OrderConfirmationModel : PurchaseModel //change this after mockup 
    {
        public int confirm (int temp) 
        {
            return 8744304 + temp;
        }

        public int orginalConfirm(int confirmation)
        {
            // return -1 to indicate an invallid confirmation nubmers
            if (confirmation < 8744304)
            {
                return -1;
            }
            return confirmation - 8744304;
        }
       
    }
}
