using System;
using System.Collections.Generic;
using ContosoOnlineOrders.Api.Models;

namespace ContosoOnlineOrders.Api.Services
{
    public interface IStoreServices
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        void UpdateProductInventory(int id, int inventory);
        void CreateProduct(Product product);
        bool CheckProductInventory(int id, int forAmount);
        void CreateOrder(Order order);
        IEnumerable<Order> GetOrders();
        bool CheckOrderInventory(Guid id);
        Order GetOrder(Guid id);
        bool ShipOrder(Guid id);
    }
}