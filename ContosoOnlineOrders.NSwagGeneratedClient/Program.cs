using System;
using System.Net.Http;

namespace ContosoOnlineOrders.NSwagGeneratedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var httpClient = new HttpClient())
            {
                var apiClient = new ContosoOnlineOrders_ApiClient(
                    "https://localhost:5001",
                    httpClient
                );

                /*
                ------------------------------------------------------------
                Without explicit operationId: Note how the generated SDK
                code for the API isn't discoverable. Each method is named
                specific to the 
                ------------------------------------------------------------
                apiClient.CheckInventoryAsync(1);   // check a product's inventory
                apiClient.InventoryAsync(1,         // update a product's inventory
                    new InventoryUpdateRequest
                    {
                        CountToAdd = 5, ProductId = 1
                    });
                apiClient.OrdersAllAsync();         // get all orders
                apiClient.OrdersAsync(1);           // get one order
                apiClient.ShipAsync(1);             // ship an order
                apiClient.ShopAllAsync();           // get all products
                apiClient.ShopAsync(new Order());   // create a new order
                apiClient.Shop2Async(1);            // get one product
                */
            }

            Console.WriteLine("Hello World!");
        }
    }
}
