using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Response> Do (Request request)
        {
            var product = _ctx.Products.FirstOrDefault(x => x.Id == request.Id);
            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Value;
            await _ctx.SaveChangesAsync();
            //return true;
            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
        
    }
}
