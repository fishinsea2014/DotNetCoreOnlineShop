using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopDomain.Models
{
    public class OrderProduct
    {
        
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
