using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoOnlineOrders.Api.Models;
using ContosoOnlineOrders.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoOnlineOrders.Api.Controllers
{
#pragma warning disable CS1998
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        public IStoreServices StoreServices { get; }

        public ShopController(IStoreServices storeServices)
        {
            StoreServices = storeServices;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
#pragma warning restore CS1998
}