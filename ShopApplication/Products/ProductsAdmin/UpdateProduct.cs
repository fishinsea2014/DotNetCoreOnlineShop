using Shop.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private ApplicationDbContext _ctx;
        public UpdateProduct (ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Do (ProductViewModel vm)
        {
            await _ctx.SaveChangesAsync();
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
        
    }
}
