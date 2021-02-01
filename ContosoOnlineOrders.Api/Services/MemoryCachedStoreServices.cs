using System;
using System.Collections.Generic;
using System.Linq;
using ContosoOnlineOrders.Api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ContosoOnlineOrders.Api.Services
{
    public class MemoryCachedStoreServices : IStoreServices
    {
        const string MEMCACHE_KEY_ORDERS = "orders";
        const string MEMCACHE_KEY_PRODUCTS = "products";

        public MemoryCachedStoreServices(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public IMemoryCache MemoryCache { get; }

        public bool CheckOrderInventory(Guid id)
        {
            var order = GetOrder(id);

            if(order == null)
            {
                throw new ArgumentException($"No order found for Order ID {id}.");
            }
            else
            {
                foreach (var cartItem in order.Items)
                {
                    var result = CheckProductInventory(cartItem.ProductId, cartItem.Quantity);
                    if(!result)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CheckProductInventory(int id, int forAmount)
        {
            return GetProduct(id).InventoryCount >= forAmount;
        }

        public void CreateOrder(Order order)
        {
            var orders = GetOrders().ToList();

            if(orders.Any(x => x.Id == order.Id))
            {
                throw new ArgumentException($"An order already exists with the Order ID {order.Id}.");
            }
            else
            {
                orders.Add(order);
                MemoryCache.Set<IEnumerable<Order>>(MEMCACHE_KEY_ORDERS, orders);
            }
        }

        public void CreateProduct(Product product)
        {
            var products = GetProducts().ToList();
            
            if(products.Any(x => x.Id == product.Id))
            {
                throw new ArgumentException($"Product with matching product ID {product.Id} already exists.");
            }
            else
            {
                products.Add(product);
                MemoryCache.Set<IEnumerable<Product>>(MEMCACHE_KEY_PRODUCTS, products);
            }
        }

        public Order GetOrder(Guid id)
        {
            return GetOrders().FirstOrDefault(_ => _.Id == id);
        }

        public IEnumerable<Order> GetOrders()
        {
            if(MemoryCache.Get(MEMCACHE_KEY_ORDERS) == null)
            {
                MemoryCache.Set<IEnumerable<Order>>(MEMCACHE_KEY_ORDERS, new List<Order>());
            }

            return MemoryCache.Get<IEnumerable<Order>>(MEMCACHE_KEY_ORDERS);
        }

        public Product GetProduct(int id)
        {
            return GetProducts().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            if(MemoryCache.Get(MEMCACHE_KEY_PRODUCTS) == null)
            {
                MemoryCache.Set<IEnumerable<Product>>(MEMCACHE_KEY_PRODUCTS, new List<Product>());
            }

            return MemoryCache.Get<IEnumerable<Product>>(MEMCACHE_KEY_PRODUCTS);
        }

        public bool ShipOrder(Guid id)
        {
            try
            {
                var orders = GetOrders();
                GetOrders().FirstOrDefault(_ => _.Id == id).IsShipped = true;
                MemoryCache.Set<IEnumerable<Order>>(MEMCACHE_KEY_ORDERS, orders);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateProductInventory(int id, int inventory)
        {
            var products = GetProducts();
            var existingProduct = products.FirstOrDefault(x => x.Id == id);
            if(existingProduct != null)
            {
                var otherProducts = products.Where(_ => _.Id != id).ToList();
                otherProducts.Add(new Product(id, existingProduct.Name, inventory));
                MemoryCache.Set<IEnumerable<Product>>(MEMCACHE_KEY_PRODUCTS, otherProducts.ToArray());
            }
        }
    }
}