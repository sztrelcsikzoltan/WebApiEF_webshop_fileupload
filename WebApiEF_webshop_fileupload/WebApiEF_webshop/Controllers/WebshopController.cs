using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiEF_webshop.Models;
using WebApiEF_webshop.Services;

namespace WebApiEF_webshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebshopController : Controller
    {
        private readonly WebshopService service;
        public WebshopController( WebshopService service)
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
        
        public  IActionResult GetProducts()
        {
            IEnumerable<DTO_Product> dTO_Products = service.GetProducts();
            return Ok(dTO_Products);
        }
        
        [HttpGet]
        [Route("/GetProduct")]
        public IActionResult GetProduct(int productId)
        {
            Product product = service.GetProduct(productId);
            return Ok(product);
        }

        [HttpGet]
        [Route("/GetProductsByCustomerId")]
        public IActionResult GetProductsByCustomerId([Required] int customerId)
        {
            IEnumerable<Product> products = service.GetProductsByCustomerId(customerId);
            return Ok(products);
        }

        [HttpGet]
        [Route("/GetOnlyProductsByCustomerId")]
        public IActionResult GetOnlyProductsByCustomerId([Required] int customerId)
        {
            IEnumerable<DTO_Product> dTO_Products = service.GetOnlyProductsByCustomerId(customerId);
            return Ok(dTO_Products);
        }

        [HttpGet]
        [Route("/GetProductsAndOrdersByCustomerId")]
        public IActionResult GetProductsAndOrdersByCustomerId([Required] int customerId)
        {
            IEnumerable<Product> products = service.GetProductsAndOrdersByCustomerId(customerId);
            return Ok(products);
        }

        [HttpGet]
        [Route("/GetProductsAndTotalAmountByCustomerId")]
        public IActionResult GetProductAndTotalAmountByProducIdAndCustomerId(int customerId)
        {
            IEnumerable<DTO_ProductAndTotalAmount> dTO_ProductAndTotalAmounts = service.GetProductsAndTotalAmountByCustomerId(customerId);
            return Ok(dTO_ProductAndTotalAmounts);
        }

        [HttpGet]
        [Route("/GetProductAndTotalAmountByProductIdAndCustomerId")]
        public IActionResult GetProductAndTotalAmountByProducIdAndCustomerId(int productId, int customerId)
        {
            DTO_ProductAndTotalAmount dTO_ProductAndTotalAmount = service.GetProductAndTotalAmountByProductIdAndCustomerId(productId, customerId);
            return Ok(dTO_ProductAndTotalAmount);
        }


        [HttpGet]
        [Route("/GetProductsAndOrderIdsByCustomerId")]
        public IActionResult GetProductsAndOrderIdsByCustomerId([Required] int customerId)
        {
            IEnumerable<DTO_ProductAndOrderIds> dTO_ProductAndOrderIds = service.GetProductsAndOrderIdsByCustomerId(customerId);
            return Ok(dTO_ProductAndOrderIds);
        }

        [HttpGet]
        [Route("/GetProductsByCustomerIdFromDate")]
        public IActionResult GetProductsByCustomerIdFromDate(int customerId, DateTime fromDate)
        {
            IEnumerable<Product> products = service.GetProductsByCustomerIdFromDate(customerId, fromDate);
            return Ok(products);
        }

        
        [HttpGet]
        [Route("/GetProductsByOrderId")]
        public IActionResult GetProductsByOrderId(int orderId)
        {
            IEnumerable<Product> products = service.GetProductsByOrderId(orderId);
            return Ok(products);
        }

        [HttpGet]
        [Route("/GetTotalProductsByCustomerIdAndProductId")]
        public IActionResult GetTotalProductsByCustomerIdAndProductId(int customerId, int productId)
        {
            string totalProducts = service.GetTotalProductsByCustomerIdAndProductId(customerId, productId);
            return Ok(totalProducts);
        }


        [HttpGet]
        [Route("/GetOrdersByCustomerId")]
        public IActionResult GetOrdersByCustomerId(int customerId)
        {
            IEnumerable<Order> orders = service.GetOrdersByCustomerId(customerId);
            return Ok(orders);
        }

        [HttpGet]
        [Route("/GetOrdersByProductName")]
        public IActionResult GetOrdersByProductName(string productName)
        {
            IEnumerable<Order> orders = service.GetOrdersByProductName(productName);
            return Ok(orders);
        }

        [HttpGet]
        [Route(("/GetOrdersByProductNameAndCustomerName"))]
        public IActionResult GetOrdersByProductNameAndCustomerName(string productName, string customerName)
        {
            IEnumerable<Order> orders = service.OrdersByProductNameAndCustomerName(productName, customerName);
            return Ok(orders);
        }

        [HttpPost]
        [Route("/AddOrder")]
        public IActionResult AddNewOrder(int customerId, int productId, int amount)
        {
            string result = service.AddOrder(customerId, productId, amount);
            return Ok(result);
        }

        [HttpPost]
        [Route("/AddProduct")]
        public IActionResult AddProduct(Product product)
        {
            string result = service.AddProduct(product);
            return Ok(result);
        }

        [HttpPost]
        [Route("/AddCustomer")]
        public IActionResult AddCustomer(Customer customer)
        {
            string result = service.AddCustomer(customer);
            return Ok(result);
        }

        [HttpPut]
        [Route("/AddProductToOrderByProductIdAndOrderId")]
        public IActionResult AddProductToOrderByProductIdAndOrderId(int orderid, int productId, int amount)
        {
            string result = service.AddProductToOrderByOrderIdAndProductId(orderid, productId, amount);
            return Ok(result);
        }

        [HttpPut]
        [Route("/CloseOrder")]
        public IActionResult CloseOrder(int orderId)
        {
            string result = service.CloseOrder(orderId);
            return Ok(result);
        }

        [HttpPut]
        [Route("/UpdateProduct")]
        public IActionResult UpdateProduct(Product product)
        {
            string result = service.UpdateProduct(product);
            return Ok(result);
        }


        [HttpDelete]
        [Route("/DeleteCustomer")]
        public IActionResult DeleteCustomer(int customerId)
        {
            string result = service.DeleteCustomer(customerId);
            return Ok(result);
        }
        
        [HttpDelete]
        [Route("/DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            string result = service.DeleteProduct(productId);
            return Ok(result);
        }
        
        [HttpDelete]
        [Route("/DeleteProductFromOrderByOrderIdAndProductId")]
        public IActionResult DeleteProductFromOrderByOrderIdAndProductId(int orderId, int productId)
        {
            string result = service.DeleteProductFromOrderByOrderIdAndProductId(orderId, productId);
            return Ok(result);
        }
        
        [HttpDelete]
        [Route("/DeleteOrder")]
        public IActionResult DeleteOrder(int orderId)
        {
            string result = service.DeleteOrder(orderId);
            return Ok(result);
        }
    }
}
