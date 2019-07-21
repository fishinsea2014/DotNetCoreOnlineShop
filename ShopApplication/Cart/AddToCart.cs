﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        private ApplicationDbContext _ctx;

        public AddToCart(ISession session, ApplicationDbContext ctx )
        {
            _session = session;
            _ctx = ctx;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }

        }

        public async Task<bool> Do(Request request)
        {
            //Get the stock to hold
            var stockToHold = _ctx.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();
            var stockOnHold = _ctx.StockOnHold.Where(x => x.SessionId == _session.Id).ToList();


            if (stockToHold.Qty < request.Qty)
            {
                //Return not enough stock
                return false;

            }
            _ctx.StockOnHold.Add(new StockOnHold
            {
                StockId = stockToHold.Id,
                SessionId= _session.Id,
                Qty = request.Qty,
                ExpiryDate = DateTime.Now.AddMinutes(20) //Hold 20 minutes
            });
            stockToHold.Qty = stockToHold.Qty - request.Qty;

            foreach(var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTime.Now.AddMinutes(20);
            }

            await _ctx.SaveChangesAsync();

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");
            
            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartList.Any(x => x.StockId == request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                cartList.Add(
                    new CartProduct
                    {
                        StockId = request.StockId,
                        Qty = request.Qty
                    });
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);
            return true;
        }
    }
}
