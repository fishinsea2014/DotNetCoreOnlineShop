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
        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(_ctx).Do());

        [HttpGet("product/{id}")]
        public IActionResult GetProduct( int id) => Ok(new GetProduct(_ctx).Do(id));

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProduct.Request vm) 
            => Ok(await new CreateProduct(_ctx).Do(vm));

        [HttpDelete("product/{id}")]
        public async Task<IActionResult> DeleteProducts(int id) => Ok(await new DeleteProduct(_ctx).Do(id));


        [HttpPut("product")]
        public async Task<IActionResult> UpdateProducts([FromBody]UpdateProduct.Request vm)
            => Ok(await new UpdateProduct(_ctx).Do(vm));

    }
}
