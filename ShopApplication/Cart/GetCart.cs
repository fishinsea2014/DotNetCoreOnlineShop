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
            public decimal RealValue { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }

        }

        public class CustomerInformation
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

        }

        public IEnumerable<Response> Do()
        {
            var stringObject = _session.GetString("cart");
            if (string.IsNullOrEmpty(stringObject))
                return new List<Response>();
            
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            
            var response = _ctx.Stock
                .Include(x =>x.Product)
                .Where(x => cartList.Any( y=> y.StockId == x.Id))
                .Select(x =>new Response {
                    Name=x.Product.Name,
                    Value=$"$ {x.Product.Value.ToString("N2")}",
                    RealValue=x.Product.Value,
                    StockId=x.Id,
                    Qty=cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                })
                .ToList();
            return response;
        }
    }
}
