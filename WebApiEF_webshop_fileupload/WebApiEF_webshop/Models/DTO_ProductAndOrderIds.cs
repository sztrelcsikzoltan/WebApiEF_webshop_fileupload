using System.Collections.Generic;

namespace WebApiEF_webshop.Models
{
    public class DTO_ProductAndOrderIds
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public int ProductPrice { get; set; }

        public IEnumerable<int> OrderId { get; set; }

        public DTO_ProductAndOrderIds(int productId, string productName, string productDescription, int productPrice, IEnumerable<int> orderId)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            OrderId = orderId;
        }
    }
}
