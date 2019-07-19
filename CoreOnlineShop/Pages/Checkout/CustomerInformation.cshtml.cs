using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace CoreOnlineShop.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            //Get Cart
            //If cart exists, then go to payment
            var information = new GetCustomerInformation(HttpContext.Session).Do();
            if (information == null)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Checkout/Payment");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Post cart
            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);
            return RedirectToPage("Payment");
        }
    }
}