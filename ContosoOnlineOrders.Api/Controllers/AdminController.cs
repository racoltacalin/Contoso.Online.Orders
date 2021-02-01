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
    public class AdminController : ControllerBase
    {
        public IStoreServices StoreServices { get; }

        public AdminController(IStoreServices storeServices)
        {
            StoreServices = storeServices;
        }

        [HttpGet("/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(StoreServices.GetOrders());
        }

        [HttpGet("/orders/{id}")]
        public async Task <ActionResult<Order>> GetOrder([FromRoute] Guid id)
        {
            var order = StoreServices.GetOrder(id);

            if(order == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(order);
            }
        }

        [HttpGet("/orders/{id}/checkInventory")]
        public async Task<ActionResult> CheckInventory([FromRoute] Guid id)
        {
            try
            {
                var result = StoreServices.CheckOrderInventory(id);
                if(result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpGet("/orders/{id}/ship")]
        public async Task<ActionResult> ShipOrder([FromRoute] Guid id)
        {
            var result = StoreServices.ShipOrder(id);
            if(result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("/products/{id}/checkInventory")]
        public async Task<ActionResult> UpdateProductInventory([FromRoute] int id, 
            [FromBody] InventoryUpdateRequest request)
        {
            try
            {
                StoreServices.UpdateProductInventory(id, request.countToAdd);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("/products")]
        public async Task<ActionResult<IEnumerable<Order>>> CreateProduct(
            [FromBody] CreateProductRequest request)
        {
            try
            {
                var newProduct = new Product(request.Id, request.Name, request.InventoryCount);
                StoreServices.CreateProduct(newProduct);
                return Created($"/products/{request.Id}", newProduct);
            }
            catch
            {
                return Conflict();
            }
        }
    }
#pragma warning restore CS1998
}