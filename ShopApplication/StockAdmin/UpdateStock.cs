using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDomain.Models;

namespace Shop.Application.StockAdmin
{
    public class UpdateStock
    {
        private ApplicationDbContext _ctx;
        public UpdateStock(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Response> Do(Request request)
        {
            var stocks = new List<Stock>();
            foreach(var s in request.Stock)
            {
                stocks.Add(new Stock {
                    Id = s.Id,
                    Description=s.Description,
                    Qty= s.Qty,
                    ProductId= s.ProductId
                });

            }
            _ctx.Stock.UpdateRange(stocks);
            await _ctx.SaveChangesAsync();            
            return new Response
            {
                Stock = request.Stock
            };
        }

        public class StockViewModel
        {
            public int Id;
            public int ProductId { get; set; }
            public string Description { get; set; }

            public int Qty { get; set; }
        }

        public class Request
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
