using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Database;
using Shop.Application.Products;
using Shop.Application.ProductsAdmin;

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
        public CreateProduct.ProductViewModel Product { get; set; } //create 

        public IEnumerable<Shop.Application.Products.GetProducts.ProductViewModel> Products { get; set; }

        public void OnGet()
        {
            Products = new Shop.Application.Products.GetProducts(_ctx).Do();

        }

        public async Task<IActionResult>  OnPost()
        {
            await new CreateProduct(_ctx).Do(Product); 
            return RedirectToPage("Index");
        }
    }
}
