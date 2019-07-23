using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.OrdersAdmin
{
    public class UpdataOrder
    {
        private ApplicationDbContext _ctx;
        public UpdataOrder (ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> Do(int id)
        {
            var order = _ctx.Orders.FirstOrDefault(x => x.Id == id);
            order.Status = order.Status + 1;
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
