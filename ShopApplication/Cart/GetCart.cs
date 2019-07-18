using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Cart
{
    public class GetCart
    {
        private ISession _session;
        private ApplicationDbContext _ctx;

        public GetCart(ISession session,ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public class Response
        {
            //Name and value of the product
            public string Name { get; set; }
            public string Value { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }

        }

        public Response Do()
        {
            //TODO: Account for multiple items in the cart.
            var stringObject = _session.GetString("cart");
            var cartProduct = JsonConvert.DeserializeObject<CartProduct>(stringObject);

            
            var response = _ctx.Stock
                .Include(x =>x.Product)
                .Where(x => x.Id == cartProduct.StockId)
                .Select(x =>new Response {
                    Name=x.Product.Name,
                    Value=$"$ {x.Product.Value.ToString("N2")}",
                    StockId=x.Id,
                    Qty=cartProduct.Qty
                })
                .FirstOrDefault();
            return response;
        }
    }
}
