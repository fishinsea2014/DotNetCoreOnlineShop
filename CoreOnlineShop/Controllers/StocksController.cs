﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.StockAdmin;
using Shop.Database;

namespace CoreOnlineShop.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        private ApplicationDbContext _ctx;
        public StocksController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        #region Stock actions
        [HttpGet("")]
        public IActionResult GetStocks() => Ok(new GetStock(_ctx).Do());

        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody]CreateStock.Request vm)
            => Ok(await new CreateStock(_ctx).Do(vm));

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteStocks(int id) => Ok(await new DeleteStock(_ctx).Do(id));


        [HttpPut("")]
        public async Task<IActionResult> UpdateStocks([FromBody]UpdateStock.Request vm)
            => Ok(await new UpdateStock(_ctx).Do(vm));
        #endregion


    }
}