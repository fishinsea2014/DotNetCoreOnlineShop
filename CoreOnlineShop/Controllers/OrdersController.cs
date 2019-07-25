using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.OrdersAdmin;
using Shop.Database;

namespace CoreOnlineShop.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        /// <summary>
        /// After inject orders class in service, we can use this class direcitly
        /// And ApplicationDbContext will injected automatically.
        /// </summary>
        /// 
        [HttpGet("")]
        public IActionResult GetOrders(int status,[FromServices] GetOrders getOrders)
        {
            return Ok(getOrders.Do(status));
        }
        

        [HttpGet("{id}")]
        public IActionResult GetOrder(
            int id,
            [FromServices]GetOrder getOrder) 
            => Ok(getOrder.Do(id));


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(
            int id,
            [FromServices] UpdateOrder updateOrder) 
            => Ok(await updateOrder.DoAsync(id));

        //Instead of :
        //private ApplicationDbContext _ctx;
        //public OrdersController(ApplicationDbContext ctx)
        //{
        //    _ctx = ctx;
        //}
        
    
        //[HttpGet("")]
        //public IActionResult GetOrders(int status) => Ok(new GetOrders(_ctx).Do(status));

        //[HttpGet("{id}")]
        //public IActionResult GetOrder(int id) => Ok(new GetOrder(_ctx).Do(id));
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateOrder(int id) => Ok(await new UpdataOrder(_ctx).Do(id));

        
    }
}