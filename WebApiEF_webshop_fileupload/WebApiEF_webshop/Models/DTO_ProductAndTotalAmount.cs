using System.Collections.Generic;

namespace WebApiEF_webshop.Models
{
    public class DTO_ProductAndTotalAmount
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProducDescription { get; set; }

        public int ProductPrice { get; set; }

        public int TotalAmount { get; set; }

        public DTO_ProductAndTotalAmount(int productId, string productName, string productDescription, int productPrice, int totalAmount)
        {
            ProductId = productId;
            ProductName = productName;
            ProducDescription = productDescription;
            ProductPrice = productPrice;
            TotalAmount = totalAmount;
        }
    }
}
