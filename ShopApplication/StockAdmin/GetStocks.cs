﻿using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.StockAdmin
{
    public class GetStocks
    {
        private ApplicationDbContext _ctx;
        public GetStocks(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<StockViewModel> Do(int productId)
        {
            var stock = _ctx.Stock
                .Where(x => x.ProductId == productId)
                .Select( x=> new StockViewModel
                {
                    Id=x.Id,
                    ProductId=x.ProductId,
                    Qty=x.Qty
                })
                .ToList();
            return stock;
        }

        public class StockViewModel
        {
            public int Id;
            public int ProductId { get; set; }
            public string Description { get; set; }

            public int Qty { get; set; }
        }
    }
}
