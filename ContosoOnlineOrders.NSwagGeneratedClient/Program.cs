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
                apiClient.CheckInventoryAsync(1);                           // check a product's inventory
                apiClient.CheckInventoryAsync(Guid.Empty)                   // check an order's inventory
                apiClient.CheckInventory2Async(1,                           // update a product's inventory
                    new InventoryUpdateRequest
                    {
                        CountToAdd = 5, ProductId = 1
                    });
                apiClient.Orders2Async(Guid.Empty);                         // get one order
                apiClient.OrdersAllAsync();                                 // get all orders
                apiClient.OrdersAsync(new Order());                         // create a new order
                apiClient.Products2Async(1);                                // get one product
                apiClient.ProductsAllAsync(new CreateProductRequest());     // create a product
                apiClient.ProductsAsync();                                  // get all products
                apiClient.ShipAsync(Guid.Empty);                            // ship an order
                */
            }

            Console.WriteLine("Hello World!");
        }
    }
}
