using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopDomain.Models
{
     public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }
        [Column(TypeName = "18,2")]
        public decimal Value  { get; set; }

    }
}
