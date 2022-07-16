using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApiEF_webshop.Models;
using WebApiEF_webshop.Services;

namespace WebApiEF_webshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebshopController : Controller
    {
        private readonly WebshopService service;
        public WebshopController(WebshopService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("/GetProducts")]
        /*
        public IEnumerable<Product> GetProducts()
        {
            return service.GetProducts();
        }
        */

        public IActionResult GetProducts()
        {
            IEnumerable<DTO_Product> dTO_Products = service.GetProducts();
            try
            {
                if (dTO_Products.ToList().Count == 0)
                {
                    return NotFound("No product was found.");
                }
                return Ok(dTO_Products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetProduct")]
        public IActionResult GetProduct([Required] int productId)
        {
            try
            {
                Product product = service.GetProduct(productId);
                if (product == null)
                {
                    return NotFound($"No product was found with id {productId}");
                }
                return Ok(product);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetProductsByCustomerId")]
        public IActionResult GetProductsByCustomerId([Required] int customerId)
        {
            try
            {
                IEnumerable<Product> products = service.GetProductsByCustomerId(customerId);
                if (products.ToList().Count == 0)
                {
                    return NotFound($"No products were found for the customer id {customerId}");
                }
                return Ok(products);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetOnlyProductsByCustomerId")]
        public IActionResult GetOnlyProductsByCustomerId([Required] int customerId)
        {
            try
            {
                IEnumerable<DTO_Product> dTO_Products = service.GetOnlyProductsByCustomerId(customerId);
                if (dTO_Products.ToList().Count == 0)
                {
                    return NotFound($"No products were found for the customer id {customerId}");
                }
                return Ok(dTO_Products);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetProductsAndOrdersByCustomerId")]
        public IActionResult GetProductsAndOrdersByCustomerId([Required] int customerId)
        {
            try
            {
                IEnumerable<Product> products = service.GetProductsAndOrdersByCustomerId(customerId);
                if (products.ToList().Count == 0)
                {
                    return NotFound($"No products/orders were found for the customer id {customerId}");
                }
                return Ok(products);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetProductsAndTotalAmountByCustomerId")]
        public IActionResult GetProductAndTotalAmountByCustomerId([Required] int customerId)
        {
            try
            {
                IEnumerable<DTO_ProductAndTotalAmount> dTO_ProductAndTotalAmounts = service.GetProductsAndTotalAmountByCustomerId(customerId);
                if (dTO_ProductAndTotalAmounts.ToList().Count == 0)
                {
                    return NotFound($"No products were found for the customer id {customerId}");
                }
                return Ok(dTO_ProductAndTotalAmounts);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetProductAndTotalAmountByProductIdAndCustomerId")]
        public IActionResult GetProductAndTotalAmountByProductIdAndCustomerId([Required] int productId, [Required] int customerId)
        {
            try
            {
                DTO_ProductAndTotalAmount dTO_ProductAndTotalAmount = service.GetProductAndTotalAmountByProductIdAndCustomerId(productId, customerId);
                if (dTO_ProductAndTotalAmount == null)
                {
                    return NotFound($"No product was found for the productId {productId} and the customer id {customerId}");
                }
                return Ok(dTO_ProductAndTotalAmount);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("/GetProductsAndOrderIdsByCustomerId")]
        public IActionResult GetProductsAndOrderIdsByCustomerId([Required] int customerId)
        {
            try
            {
                IEnumerable<DTO_ProductAndOrderIds> dTO_ProductAndOrderIds = service.GetProductsAndOrderIdsByCustomerId(customerId);
                if (dTO_ProductAndOrderIds.ToList().Count == 0)
                {
                    return NotFound($"No products/orders were found for the customer id {customerId}");
                }
                return Ok(dTO_ProductAndOrderIds);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetProductsByCustomerIdFromDate")]
        public IActionResult GetProductsByCustomerIdFromDate([Required] int customerId, DateTime fromDate)
        {
            try
            {
                IEnumerable<Product> products = service.GetProductsByCustomerIdFromDate(customerId, fromDate);
                if (products.ToList().Count == 0)
                {
                    return NotFound($"No products were found for the customer id {customerId} from date {fromDate}");
                }
                return Ok(products);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("/GetProductsByOrderId")]
        public IActionResult GetProductsByOrderId([Required] int orderId)
        {
            try
            {
                IEnumerable<Product> products = service.GetProductsByOrderId(orderId);
                if (products.ToList().Count == 0)
                {
                    return NotFound($"No products were found for the order id {orderId}");
                }
                return Ok(products);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetTotalProductsByCustomerIdAndProductId")]
        public IActionResult GetTotalProductsByCustomerIdAndProductId([Required] int customerId, [Required] int productId)
        {
            try
            {
                string totalProducts = service.GetTotalProductsByCustomerIdAndProductId(customerId, productId);
                if (totalProducts == "0")
                {
                    return NotFound($"No product was found for the customer id {customerId} and for the product id {productId}");
                }
                return Ok(totalProducts);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("/GetOrdersByCustomerId")]
        public IActionResult GetOrdersByCustomerId([Required] int customerId)
        {
            try
            {
                IEnumerable<Order> orders = service.GetOrdersByCustomerId(customerId);
                if (orders.ToList().Count == 0)
                {
                    return NotFound($"No order was found for the customer id {customerId}");
                }
                return Ok(orders);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetOrdersByProductName")]
        public IActionResult GetOrdersByProductName([Required] string productName)
        {
            try
            {
                IEnumerable<Order> orders = service.GetOrdersByProductName(productName);
                if (orders.ToList().Count == 0)
                {
                    return NotFound($"No order was found for the product name '{productName}'");
                }
                return Ok(orders);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route(("/GetOrdersByProductNameAndCustomerName"))]
        public IActionResult GetOrdersByProductNameAndCustomerName([Required] string productName, [Required] string customerName)
        {
            try
            {
                IEnumerable<Order> orders = service.OrdersByProductNameAndCustomerName(productName, customerName);
                if (orders.ToList().Count == 0)
                {
                    return NotFound($"No orders were found for the product name '{productName}' and for the customer name '{customerName}'");
                }
                return Ok(orders);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/AddOrder")]
        public IActionResult AddOrder([Required] int customerId, [Required] int productId, [Required] int amount)
        {
            try
            {
                string result = service.AddOrder(customerId, productId, amount);
                if (result.Contains("does no exist") || result.Contains("greater than zero"))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/AddProduct")]
        public IActionResult AddProduct(DTO_ProductNoId dTO_ProductNoId)
        {
            try
            {
                string result = service.AddProduct(dTO_ProductNoId);
                if (result.Contains("already exists") || result.Contains("Please enter"))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/AddCustomer")]
        public IActionResult AddCustomer(DTO_CustomerNoId dTO_CustomerNoId)
        {
            try
            {
                string result = service.AddCustomer(dTO_CustomerNoId);
                if (result.Contains("already exists") || result.Contains("Please enter"))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/AddProductToOrderByProductIdAndOrderId")]
        public IActionResult AddProductToOrderByProductIdAndOrderId([Required] int productId, [Required] int orderid, [Required] int amount)
        {
            try
            {
                string result = service.AddProductToOrderByProductIdAndOrderId(productId, orderid, amount);
                if (result.Contains("We") == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/CloseOrder")]
        public IActionResult CloseOrder([Required] int orderId)
        {
            try
            {
                string result = service.CloseOrder(orderId);
                if (result.Contains("does not exist"))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/UpdateProduct")]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                string result = service.UpdateProduct(product);
                if (result.Contains("does not exist"))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("/DeleteCustomer")]
        public IActionResult DeleteCustomer(int customerId)
        {
            try
            {
                string result = service.DeleteCustomer(customerId);
                if (result.Contains("was deleted") == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                string result = service.DeleteProduct(productId);
                if (result.Contains("was deleted") == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/DeleteProductFromOrderByOrderIdAndProductId")]
        public IActionResult DeleteProductFromOrderByOrderIdAndProductId(int orderId, int productId)
        {
            try
            {
                string result = service.DeleteProductFromOrderByOrderIdAndProductId(orderId, productId);
                if (result.Contains("does not exist"))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/DeleteOrder")]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                string result = service.DeleteOrder(orderId);
                if (result.Contains("does not exist"))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
