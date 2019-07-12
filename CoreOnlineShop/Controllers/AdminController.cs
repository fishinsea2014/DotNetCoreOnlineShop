using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOnlineShop.Controllers
{
    [Route("[controller]")]
    public class AdminController: Controller
    {
        
        private ApplicationDbContext _ctx;
        public AdminController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet("product")]
        public IActionResult GetProducts() => Ok(new GetProducts(_ctx).Do());

        [HttpGet("product/{id}")]
        public IActionResult GetProduct( int id) => Ok(new GetProduct(_ctx).Do(id));

        [HttpPost("product")]
        public IActionResult CreateProducts(CreateProduct.ProductViewModel vm) 
            => Ok(new CreateProduct(_ctx).Do(vm));

        [HttpDelete("product/{id}")]
        public IActionResult DeleteProducts(int id) => Ok(new DeleteProduct(_ctx).Do(id));


        [HttpPut("product")]
        public IActionResult UpdateProducts(UpdateProduct.ProductViewModel vm)
            => Ok(new UpdateProduct(_ctx).Do(vm));

    }
}
