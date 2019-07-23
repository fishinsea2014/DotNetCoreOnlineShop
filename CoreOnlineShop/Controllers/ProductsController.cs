using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Shop.Application.Products;
using Shop.Application.ProductsAdmin;

using Shop.Database;

namespace CoreOnlineShop.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _ctx;
        public ProductsController (ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        #region Products actions
        [HttpGet("")]
        public IActionResult GetProducts() => Ok(new GetProducts(_ctx).Do());

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_ctx).Do(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProduct.Request vm)
            => Ok(await new CreateProduct(_ctx).Do(vm));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int id) => Ok(await new DeleteProduct(_ctx).Do(id));


        [HttpPut("")]
        public async Task<IActionResult> UpdateProducts([FromBody]UpdateProduct.Request vm)
        {
            return Ok(await new UpdateProduct(_ctx).Do(vm));
        }
        #endregion
    }
}