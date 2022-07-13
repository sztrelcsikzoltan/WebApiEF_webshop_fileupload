using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiEF_webshop.Models;

namespace WebApiEF_webshop.Services
{
    public class WebshopService
    {
        private readonly webshop_fileuploadContext context;

        public WebshopService(webshop_fileuploadContext context)
        {
            this.context = context;
        }

        /*
        public IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }
        */
        
        public IEnumerable<DTO_Product> GetProducts()
        {
            return context.Products
                .Select(p => new DTO_Product(p.Id, p.Name, p.Description, p.Price, p.Imglink));
        }
        
        public Product GetProduct(int productId)
        {
            return context.Products.Where(p => p.Id == productId).FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCustomerId(int customerId)
        {
            /*
            IEnumerable<Order> orders = context.Orders // orderek szűrése customerId alapján, ÉS OrderProducts bevonása, onnan Product bevonása
                .Where(o => o.CustomerId == customerId)
                .Include(x => x.OrderProducts)
                .ThenInclude(y => y.Product);

            List<Product> products = new List<Product>();
            foreach (Order order in orders) // iterálás orderekben, a termékek kigyűjtése céljából
            {
                foreach (OrderProduct orderProduct in order.OrderProducts) // iterálás OrderProductban
                {
                    products.Add(orderProduct.Product); // termék kigyűjtése
                }
            }
            */
            // Products → (OrderProducts → Order.CustomerId) (SINGLE results)
            
            var products = context.Products.Where(p => p.OrderProducts.Any(op => op.Order.CustomerId == customerId));
            // var products1 = context.Products.Where(p => p.OrderProducts.Any(op => op.Order.Customer.Orders.Any(o => o.Customer.Id == customerId)));

            // Orders → OrderProducts → Products (MULTIPLE results)
            /*
            var products = context.Orders.Where(o => o.CustomerId == customerId)
                //.Include(op => op.OrderProducts)
                .SelectMany(p => p.OrderProducts)
                .Select(p => p.Product);
            */

            // OrderProducts → Order.CustomerId → select Product (MULTIPLE results)
            /*
            var products = context.OrderProducts.Where(op => op.Order.CustomerId == customerId)
                .Select(op => op.Product);
            */
            // Customers → Orders → OrderProducts → Product (MULTIPLE results)
            /*
            var products = context.Customers.Where(c => c.Id == customerId)
                .SelectMany(c => c.Orders)
                .SelectMany(o => o.OrderProducts)
                .Select(op => op.Product);
            */
            /*
            var products = context.Customers.SelectMany(c => c.Orders)
                .Where(o => o.CustomerId == customerId)
                .SelectMany(o => o.OrderProducts)
                .Select(op => op.Product);
            */

            return products;
        }

        public IEnumerable<DTO_Product> GetOnlyProductsByCustomerId(int customerId)
        {
            // Products → (OrderProducts → Order.CustomerId) (SINGLE results)
            /*            
            var products = context.Products.Where(p => p.OrderProducts
               .Any(op => op.Order.CustomerId == customerId))
               .Select(p => new DTO_Product(p.Id, p.Name, p.Price));
            */
            
            var products = context.OrderProducts.Where(op => op.Order.CustomerId == customerId).
                Select(op => new DTO_Product(op.ProductId, op.Product.Name, op.Product.Description, op.Product.Price, op.Product.Imglink));
            
            return products;
        }

        public IEnumerable<Product> GetProductsAndOrdersByCustomerId(int customerId)
        {
            var products = context.Products.Where(p => p.OrderProducts.Any(op => op.Order.CustomerId == customerId))
                .Include(p => p.OrderProducts);

            return products;
        }

        public IEnumerable<DTO_ProductAndTotalAmount> GetProductsAndTotalAmountByCustomerId(int customerId)
        {
            // Products → (OrderProducts → Order.CustomerId and ProductId) (SINGLE results for products, sum for Amount in OrderProducts

            var products = context.Products.Where(p => p.OrderProducts.Any(op => op.Order.CustomerId == customerId)).ToList();

            List<DTO_ProductAndTotalAmount> dtoList = new List<DTO_ProductAndTotalAmount>();
            for (int i = 0; i < products.Count; i++)
            {
                int totalAmount = context.OrderProducts.Where(op => op.ProductId == products[i].Id && op.Order.CustomerId == customerId).Sum(op => op.Amount);

                dtoList.Add(new DTO_ProductAndTotalAmount(products[i].Id, products[i].Name, products[i].Description, products[i].Price, totalAmount));
            }

            return dtoList;
        }

        public DTO_ProductAndTotalAmount GetProductAndTotalAmountByProductIdAndCustomerId(int productId, int customerId)
        {
            // Products → (OrderProducts → Order.CustomerId and ProductId) (SINGLE results for products, sum for Amount in OrderProducts

            var products = context.Products.Where(p => p.Id == productId)
                .Where(p => p.OrderProducts.Any(op => op.Order.CustomerId == customerId)).
                Select(p => new DTO_ProductAndTotalAmount(p.Id, p.Name, p.Description, p.Price, context.OrderProducts.Where(op => op.Order.CustomerId == customerId && op.ProductId == productId)
                .Sum(op => op.Amount)))
                .FirstOrDefault();

            return products;
        }

        public IEnumerable<DTO_ProductAndOrderIds> GetProductsAndOrderIdsByCustomerId(int customerId)
        {
            // Products → (OrderProducts → Order.CustomerId) (SINGLE results for products, BUT multiple orders as they were used in the selection!)
            var products = context.Products.Where(p => p.OrderProducts
               .Any(op => op.Order.CustomerId == customerId))
               // .Include(p => p.OrderProducts)
               .Select(p => new DTO_ProductAndOrderIds(p.Id, p.Name,p.Description, p.Price, p.OrderProducts.Select(op => op.OrderId)));

            return products;
        }


        public IEnumerable<Product> GetProductsByCustomerIdFromDate(int customerId, DateTime fromDate)
        {
            // Products → OrderProducts → Order.customerId + Order.CreatedOn (SINGLE results)

            var products = context.Products.Where(p => p.OrderProducts
                .Any(op => op.Order.CustomerId == customerId && op.Order.CreatedOn >= fromDate));

            // Orders: order.customerId + Order.CreatedOn → OrderProducts → Products
            /*
            var products = context.Orders.Where(o => o.CustomerId == customerId && o.CreatedOn > fromDate)
                .SelectMany(o => o.OrderProducts)
                .Select(op => op.Product);
            */
            // OrderProducts.Order.customerId + .Order.CreatedOn → Products
            /*
            var products = context.OrderProducts.Where(op => op.Order.CustomerId == customerId && op.Order.CreatedOn > fromDate)
                .Select(op => op.Product);
            */

            // Customers.Id → Orders.CreatedOn → OrderProducts → Products
            /*
            var products = context.Customers.Where(c => c.Id == customerId)
                .SelectMany(c => c.Orders.Where(o => o.CreatedOn > fromDate))
                .SelectMany(o => o.OrderProducts)
                .Select(op => op.Product);
            */

            return products;
        }

        public IEnumerable<Product> GetProductsByOrderId(int orderId)
        {
            // Products → OrderProducts → Orders (orderId)
            var products = context.Products.Where(p => p.OrderProducts
            .Any(op => op.OrderId == orderId));

            return products;
        }

        public string GetTotalProductsByCustomerIdAndProductId(int customerId, int productId)
        {
            // az OrderProduct-ból kell kiindulni, hogy legynek ismétlődések!

            bool customerExists = context.Customers.Any(c => c.Id == customerId);
            if (customerExists == false) { return $"Customer with id {customerId} does not exist!"; }
            bool productExists = context.Products.Any(p => p.Id == productId);
            if (productExists == false) { return $"Product with id {productId} does not exist!"; }

            return context.OrderProducts.Where(op => op.Product.Id == productId)
                .Where(op => op.Order.Customer.Id == customerId).Sum(op => op.Amount).ToString();
        }

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            // Orders: Order.customerId
            var orders = context.Orders.Where(o => o.CustomerId == customerId);

            return orders;
        }

        public IEnumerable<Order> GetOrdersByProductName(string productName)
        {
            // Orders → OrderProducts: Product.Name (SINGLE results)
            /*
            var orders = context.Orders.Where(o => o.OrderProducts.Any(op => op.Product.Name == productName));
            */
            // OrderProducts → Product.Name → Orders (multiple results)
            var orders = context.OrderProducts.Where(op => op.Product.Name == productName)
                .Select(op => op.Order);

            return orders;
        }

        public IEnumerable<Order> OrdersByProductNameAndCustomerName(string productName, string CustomerName)
        {
            var orders = context.Orders.Where(o => o.OrderProducts.Any(op => op.Product.Name == productName)).Where(o => o.Customer.Name == CustomerName);

            return orders;
        }


        public string AddOrder(int customerId, int productId, int amount) // a .PaidOn mezőt ki kell hagyni(null lesz), addig adható hozzá még termék, amíg nem NULL, és a PaidOn beállítása után már nem adható hozzá termék!
        {
            bool customerExists = context.Customers.Any(c => c.Id == customerId);
            if (customerExists == false) { return $"Customer with Id '{customerId}' does not exist!"; }
            bool productExists = context.Products.Any(p => p.Id == productId);
            if (productExists == false) { return $"Product with Id '{productId}' does not exist!"; }
            if (amount < 1) { return $"Amount must be greater than zero!"; }

            Order order = new Order
            {
                CreatedOn = DateTime.Now,
                CustomerId = customerId
            };
            context.Orders.Add(order);
            context.SaveChanges();

            OrderProduct orderProduct = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = productId,
                Amount = amount
            };
            context.OrderProducts.Add(orderProduct);
            context.SaveChanges();

            return $"Order with Id {order.Id} was added.";
        }

        public string AddProduct(DTO_ProductNoId dTO_ProductNoId)
        {
            if (dTO_ProductNoId.Name.Length < 4) { return $"Please enter a valid Name! (Minimum 4 characters)"; }
            bool productNameExists = context.Products.Any(p => p.Name == dTO_ProductNoId.Name);
            if (productNameExists)
            {
                return $"The product with the name '{dTO_ProductNoId.Name}' already exists!";
            }
            if (dTO_ProductNoId.Description.Length < 10) { return $"Please enter a valid Description! (Minimum 10 characters)"; }
            if (dTO_ProductNoId.Price < 1) { return $"Please enter a valid Price! (Greater than zero)" ; }
            if (dTO_ProductNoId.Imglink.Length < 4) { return $"Please enter a valid Imglink! (Minimum 4 characters)"; }

            Product product = new Product()
            {
                Name = dTO_ProductNoId.Name,
                Description = dTO_ProductNoId.Description,
                Price = dTO_ProductNoId.Price,
                Imglink = dTO_ProductNoId.Imglink
            };

            context.Products.Add(product);
            context.SaveChanges();
            return $"The product '{product.Name}' was added.";
        }
        /*
        public IEnumerable<Product> GetProductsByCustomerId2(int customerId) {
            //return context.Orders.Where(o => o.CustomerId == customerId).Select(o => o.OrderProducts.Select(op => op.Product).Select(product => new Product()
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Descroiption,
            //    Price = product.Price

            //})).ToList();
            Customer customer = context.Customers.Where(c => c.Id == customerId).First();
            var myList = customer.Orders.Select(o => o.OrderProducts.Select(op => op.Product)).Select(p => new Product() { 
                Id = 
            });
            return myList;
        }
        */

        public string AddCustomer(DTO_CustomerNoId dTO_CustomerNoId)
        {
            bool emailExists = context.Customers.Any(c => c.Email == dTO_CustomerNoId.Email);
            if (emailExists)
            {
                return $"The customer with the e-mail '{dTO_CustomerNoId.Email}' already exists!";
            }

            Customer customer = new Customer()
            {
                Name = dTO_CustomerNoId.Name,
                Email = dTO_CustomerNoId.Email
            };

            context.Customers.Add(customer);
            context.SaveChanges();
            return $"Customer '{customer.Name}' was added.";
        }


        public string AddProductToOrderByOrderIdAndProductId(int orderId, int productId, int amount)
        {

            Product product = context.Products.Find(productId);
            if (product == null) { return $"Product with Id {productId} does not exist!"; }
            Order order = context.Orders.Find(orderId);
            if (order == null) { return $"Order with Id {orderId} does not exist!"; }


            if (amount < 1) { return $"Amount must be at least 1!"; }
            else if (order.PaidOn != null) { return $"Order with Id {orderId} is closed, no more product can be addded!"; }


            OrderProduct orderProduct = context.OrderProducts.Where(op => op.ProductId == productId && op.OrderId == orderId).FirstOrDefault();

            if (orderProduct != null) // update orderProduct if exists
            {
                orderProduct.Amount += amount;
                context.OrderProducts.Update(orderProduct);
                context.SaveChanges();
                return ($"We updated the product with Id {productId} in the order with Id {orderId}.");
            }
            else // add orderProduct if does not exist
            {
                orderProduct = new OrderProduct
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Amount = amount
                };
                context.OrderProducts.Add(orderProduct);
                context.SaveChanges();
                return ($"We added the product with Id {productId} to the order with Id {orderId}.");
            }
        }

        public string CloseOrder(int orderId)
        {
            Order order = context.Orders.Find(orderId);
            if (order == null) { return $"Order with Id {orderId} does not exist!"; }

            order.PaidOn = DateTime.Now;
            context.Update(order);
            context.SaveChanges();

            return $"Order with Id {orderId} was updated.";
        }

        public string UpdateProduct(Product product)
        {
            bool productExists = context.Products.Any(p => p.Id == product.Id);
            if (productExists == false) { return $"Product with Id {product.Id} does not exist!"; }
            
            context.Products.Update(product);
            context.SaveChanges();
            return $"The product '{product.Name}' was updated.";
        }

        public string DeleteCustomer(int customerId)
        {
            Customer customer = context.Customers.Find(customerId);
            if (customer == null) { return $"Customer with Id {customerId} does not exist!"; }
            if (context.Orders.Any(o => o.CustomerId == customerId)) { return $"Customer with id {customerId} has orders, so this customer cannot be deleted. Fist delete the corresponding orders."; }
            context.Customers.Remove(customer);
            context.SaveChanges();
            return $"Customer with Id {customerId} was deleted.";
        }

        public string DeleteProduct(int productId)
        {
            Product product = context.Products.Find(productId);
            if (product == null) { return $"Product with Id {productId} does not exist!"; }
            if (context.OrderProducts.Any(op => op.Product.Id == productId)) { return $"Product with id {productId} is contained in one or more orders, so this product cannot be deleted. Fist delete the corresponding orders."; }
            context.Products.Remove(product);
            context.SaveChanges();
            return $"Product with Id {productId} was deleted.";
        }

        
        public string DeleteProductFromOrderByOrderIdAndProductId(int orderId, int productId)
        {
            Order order = context.Orders.Find(orderId);
            if (order == null) { return $"Order with Id {orderId} does not exist!"; }

            OrderProduct orderProduct = context.OrderProducts.Where(op => op.OrderId == orderId && op.ProductId == productId).FirstOrDefault();

            string message = "";
            if (orderProduct != null)
            {
                context.OrderProducts.Remove(orderProduct);
                context.SaveChanges();
                message = $"Product with Id {productId} was removed from the order.\n";

                bool orderProductsExists = context.OrderProducts.Any(op => op .OrderId == orderId && op.ProductId == productId);
                if (orderProductsExists == false)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                    message += $"Empty order with Id {orderId} was removed.";
                }
                return message;
            }
            else
            {
                return $"No product was found in the order with id {orderId}!";
            }
        }

        public string DeleteOrder(int orderId)
        {
            Order order = context.Orders.Find(orderId);
            if (order == null) { return $"Order with Id {orderId} does not exist!"; }
            IEnumerable<OrderProduct> orderProductList = context.OrderProducts.Where(op => op.OrderId == orderId);

            string message = "";
            foreach (OrderProduct orderproduct in orderProductList)
            {
                context.OrderProducts.Remove(orderproduct);
                context.SaveChanges();
                message += $"Product with Id {orderproduct.ProductId}' was removed from order.\n";
            }
            context.Orders.Remove(order);
            context.SaveChanges();
            return message += $"Order with Id {order.Id}' was removed.";
        }

    }
}
