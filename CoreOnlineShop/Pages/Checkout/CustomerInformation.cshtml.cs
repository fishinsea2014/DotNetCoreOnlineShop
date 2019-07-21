using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace CoreOnlineShop.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private IHostingEnvironment _env;

        public CustomerInformationModel (IHostingEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            //Get Cart
            //If cart exists, then go to payment
            var information = new GetCustomerInformation(HttpContext.Session).Do();
            if (information == null)
            {
                if (_env.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request
                    {
                        FirstName = "ff",
                        LastName = "ll",
                        Email = "ee@ee",
                        PhoneNumber = "0125421",
                        Address1 = "add1",
                        Address2 = "add2",
                        City = "nz city",
                        PostCode = "5510",
                    };
                }
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