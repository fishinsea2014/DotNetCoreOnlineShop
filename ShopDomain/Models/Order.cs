using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderRef { get; set; }
        public string StripeReference { get; set; }


        #region Customer Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; } 
        #endregion

        public ICollection<OrderStock> OrderStocks { get; set; }
    }
}
