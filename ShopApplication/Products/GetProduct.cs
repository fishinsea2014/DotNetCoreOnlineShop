﻿using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products
{
    public class GetProduct
    {
        private ApplicationDbContext _ctx;
        public GetProduct(ApplicationDbContext ctx)
        {
            _ctx = ctx;

        }

        public async Task<ProductViewModel> Do(string name)
        {
            var stockOnHold = _ctx.StockOnHold.Where(x => x.ExpiryDate < DateTime.Now).ToList();
            if (stockOnHold.Count >0)
            {
                var stockToReturn = _ctx.Stock.Where(x => stockOnHold.Any(y => y.StockId == x.Id)).ToList();
                foreach(var stock in stockToReturn)
                {
                    stock.Qty = stock.Qty + stockOnHold.FirstOrDefault(x => x.StockId == stock.Id).Qty;
                }
                _ctx.StockOnHold.RemoveRange(stockOnHold);
                await _ctx.SaveChangesAsync();
            }

            return _ctx.Products
                    .Include(x => x.Stock)
                    .Where(x => x.Name == name)
                    .Select(x => new ProductViewModel
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Value = $"${x.Value.ToString("N2")}", // 1100.50 -->
                        Stock = x.Stock.Select(y => new StockModelView
                        {
                            Id = y.Id,
                            Description=y.Description,
                            Qty = y.Qty 
                        })
                    })
                    .FirstOrDefault();
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockModelView> Stock { get; set; }
        }

        public class StockModelView
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
