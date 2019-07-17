using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Products
{
    public class GetProduct
    {
        private ApplicationDbContext _ctx;
        public GetProduct(ApplicationDbContext ctx)
        {
            _ctx = ctx;

        }

        public ProductViewModel Do(string name)
        {
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
                            InStock = y.Qty > 0
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
            public bool InStock { get; set; }
        }
    }
}
