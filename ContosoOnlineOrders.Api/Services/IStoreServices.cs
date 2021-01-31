using System.Collections.Generic;
using ContosoOnlineOrders.Api.Models;

namespace ContosoOnlineOrders.Api.Services
{
    public interface IStoreServices
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        void UpdateProduct(int id, Product product);
        void CreateProduct(Product product);
        bool CheckProductInventory(int id);
        void CreateOrder(Order order);
        IEnumerable<Order> GetOrders();
        bool CheckOrderInventory(int id);
        Order GetOrder(int id);
        bool ShipOrder(int id);
    }
}