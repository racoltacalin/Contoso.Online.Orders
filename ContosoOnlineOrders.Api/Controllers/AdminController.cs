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
    public class AdminController : ControllerBase
    {
        public IStoreServices StoreServices { get; }

        public AdminController(IStoreServices storeServices)
        {
            StoreServices = storeServices;
        }

#if OperationId
        [HttpGet("/orders", Name = nameof(GetOrders))]
#else
        [HttpGet("/orders")]
#endif
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(StoreServices.GetOrders());
        }

#if OperationId
        [HttpGet("/orders/{id}", Name = nameof(GetOrder))]
#else
        [HttpGet("/orders/{id}")]
#endif
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

#if OperationId
        [HttpGet("/orders/{id}/checkInventory", Name = nameof(CheckInventory))]
#else
        [HttpGet("/orders/{id}/checkInventory")]
#endif
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

#if OperationId
        [HttpGet("/orders/{id}/ship", Name = nameof(ShipOrder))]
#else
        [HttpGet("/orders/{id}/ship")]
#endif
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

#if OperationId
        [HttpPut("/products/{id}/checkInventory", Name = nameof(UpdateProductInventory))]
#else
        [HttpPut("/products/{id}/checkInventory")]
#endif
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

#if OperationId
        [HttpPost("/products", Name = nameof(CreateProduct))]
#else
        [HttpPost("/products")]
#endif
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