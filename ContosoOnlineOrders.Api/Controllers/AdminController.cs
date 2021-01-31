using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoOnlineOrders.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoOnlineOrders.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            throw new NotImplementedException();
        }

        [HttpGet("orders/{id}")]
        public async Task <ActionResult<Order>> GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("orders/{orderId}/checkInventory")]
        public async Task<ActionResult> CheckInventory(int orderId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("orders/{orderId}/ship")]
        public async Task<ActionResult> ShipOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("products/{productId}/inventory")]
        public async Task<ActionResult> UpdateProductInventory([FromRoute] int productId, 
            [FromBody] InventoryUpdateRequest request)
        {
            throw new NotImplementedException();
        }
        
    }
}