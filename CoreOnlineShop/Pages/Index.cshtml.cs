using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.CreateProducts;
using Shop.Database;
using ShopApplication.GetProducts;

namespace CoreOnlineShop.Pages
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _ctx;

        public IndexModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [BindProperty]
        public Shop.Application.CreateProducts.ProductViewModel Product { get; set; }

        public IEnumerable<ShopApplication.GetProducts.ProductViewModel> Products { get; set; }

        public void OnGet()
        {
            Products = new GetProducts(_ctx).Do();

        }

        public async Task<IActionResult>  OnPost()
        {
            await new CreateProduct(_ctx).Do(Product); 
            return RedirectToPage("Index");
        }
    }
}
