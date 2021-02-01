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

        [HttpPost("/orders")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                StoreServices.CreateOrder(order);
                return Created($"/orders/{order.Id}", order);
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpGet("/products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return new JsonResult(StoreServices.GetProducts());
            //return Ok(StoreServices.GetProducts());
        }

        [HttpGet("/products/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = StoreServices.GetProduct(id);

            if(product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }
    }
#pragma warning restore CS1998
}