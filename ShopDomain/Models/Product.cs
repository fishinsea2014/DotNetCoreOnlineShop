using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomain.Models
{
     public class Product
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }
        public decimal value  { get; set; }

    }
}
