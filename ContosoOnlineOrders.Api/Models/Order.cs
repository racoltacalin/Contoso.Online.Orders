using System;
using System.Collections.Generic;

namespace ContosoOnlineOrders.Api.Models
{
    public record Order(Guid Id, List<CartItem> Items)
    {
        public bool IsShipped { get; set; } = false;
    }
}