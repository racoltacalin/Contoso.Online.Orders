using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using ContosoOnlineOrders.Api.Models;
using ContosoOnlineOrders.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoOnlineOrders.Api.Controllers
{
#pragma warning disable CS1998
    [Route("[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ShopController : ControllerBase
    {
        public IStoreServices StoreServices { get; }

        public ShopController(IStoreServices storeServices)
        {
            StoreServices = storeServices;
        }

#if OperationId
        [HttpPost("/orders", Name = nameof(CreateOrder))]
#else
        [HttpPost("/orders")]
#endif
        public async Task<ActionResult<Order>> CreateOrder(Order order)
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

#if OperationId
        [HttpGet("/products", Name = nameof(GetProducts))]
#else
        [HttpGet("/products")]
#endif
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(StoreServices.GetProducts());
        }

#if OperationId
        [HttpGet("/products/{id}", Name = nameof(GetProduct))]
#else
        [HttpGet("/products/{id}")]
#endif
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