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
        
        public IEnumerable<DTO_Product> GetProducts()
        {
            return service.GetProducts();
        }
        
        [HttpGet]
        [Route("/GetProduct")]
        public Product GetProduct(int productId)
        {
            return service.GetProduct(productId);
        }

        [HttpGet]
        [Route("/GetProductsByCustomerId")]
        public IEnumerable<Product> GetProductsByCustomerId([Required] int customerId)
        {
            return service.GetProductsByCustomerId(customerId);
        }

        [HttpGet]
        [Route("/GetOnlyProductsByCustomerId")]
        public IEnumerable<DTO_Product> GetOnlyProductsByCustomerId([Required] int customerId)
        {
            return service.GetOnlyProductsByCustomerId(customerId);
        }

        [HttpGet]
        [Route("/GetProductsAndOrdersByCustomerId")]
        public IEnumerable<Product> GetProductsAndOrdersByCustomerId([Required] int customerId)
        {
            return service.GetProductsAndOrdersByCustomerId(customerId);
        }

        [HttpGet]
        [Route("/GetProductsAndTotalAmountByCustomerId")]
        public IEnumerable<DTO_ProductAndTotalAmount> GetProductAndTotalAmountByProducIdAndCustomerId(int customerId)
        {
            return service.GetProductsAndTotalAmountByCustomerId(customerId);
        }

        [HttpGet]
        [Route("/GetProductAndTotalAmountByProductIdAndCustomerId")]
        public DTO_ProductAndTotalAmount GetProductAndTotalAmountByProducIdAndCustomerId(int productId, int customerId)
        {
            return service.GetProductAndTotalAmountByProductIdAndCustomerId(productId, customerId);
        }


        [HttpGet]
        [Route("/GetProductsAndOrderIdsByCustomerId")]
        public IEnumerable<DTO_ProductAndOrderIds> GetProductsAndOrderIdsByCustomerId([Required] int customerId)
        {
            return service.GetProductsAndOrderIdsByCustomerId(customerId);
        }

        [HttpGet]
        [Route("/GetProductsByCustomerIdFromDate")]
        public IEnumerable<Product> GetProductsByCustomerIdFromDate(int customerId, DateTime fromDate)
        {
            return service.GetProductsByCustomerIdFromDate(customerId, fromDate);
        }

        
        [HttpGet]
        [Route("/GetProductsByOrderId")]
        public IEnumerable<Product> GetProductsByOrderId(int orderId)
        {
            return service.GetProductsByOrderId(orderId);
        }

        [HttpGet]
        [Route("/GetTotalProductsByCustomerIdAndProductId")]
        public string GetTotalProductsByCustomerIdAndProductId(int customerId, int productId)
        {
            return service.GetTotalProductsByCustomerIdAndProductId(customerId, productId);
        }


        [HttpGet]
        [Route("/GetOrdersByCustomerId")]
        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            return service.GetOrdersByCustomerId(customerId);
        }

        [HttpGet]
        [Route("/GetOrdersByProductName")]
        public IEnumerable<Order> GetOrdersByProductName(string productName)
        {
            return service.GetOrdersByProductName(productName);
        }

        [HttpGet]
        [Route(("/GetOrdersByProductNameAndCustomerName"))]
        public IEnumerable<Order> GetOrdersByProductNameAndCustomerName(string productName, string customerName)
        {
            return service.OrdersByProductNameAndCustomerName(productName, customerName);
        }

        [HttpPost]
        [Route("/AddOrder")]
        public string AddNewOrder(int customerId, int productId, int amount)
        {
            return service.AddOrder(customerId, productId, amount);
        }

        [HttpPost]
        [Route("/AddProduct")]
        public string AddProduct(Product product)
        {
            return service.AddProduct(product);
        }

        [HttpPost]
        [Route("/AddCustomer")]
        public string AddCustomer(Customer customer)
        {
            return service.AddCustomer(customer);
        }

        [HttpPut]
        [Route("/AddProductToOrderByProductIdAndOrderId")]
        public string AddProductToOrderByProductIdAndOrderId(int orderid, int productId, int amount)
        {
            return service.AddProductToOrderByOrderIdAndProductId(orderid, productId, amount);
        }

        [HttpPut]
        [Route("/CloseOrder")]
        public string CloseOrder(int orderId)
        {
            return service.CloseOrder(orderId);
        }

        [HttpPut]
        [Route("/UpdateProduct")]
        public string UpdateProduct(Product product)
        {
            return service.UpdateProduct(product);
        }


        [HttpDelete]
        [Route("/DeleteCustomer")]
        public string DeleteCustomer(int customerId)
        {
            return service.DeleteCustomer(customerId);
        }
        
        [HttpDelete]
        [Route("/DeleteProduct")]
        public string DeleteProduct(int productId)
        {
            return service.DeleteProduct(productId);
        }
        
        [HttpDelete]
        [Route("/DeleteProductFromOrderByOrderIdAndProductId")]
        public string DeleteProductFromOrderByOrderIdAndProductId(int orderId, int productId)
        {
            return service.DeleteProductFromOrderByOrderIdAndProductId(orderId, productId);
        }
        
        [HttpDelete]
        [Route("/DeleteOrder")]
        public string DeleteOrder(int orderId)
        {
            return service.DeleteOrder(orderId);
        }
    }
}
