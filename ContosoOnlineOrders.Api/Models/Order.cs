using System;
using System.Collections.Generic;

namespace ContosoOnlineOrders.Api.Models
{
    public record Order(Guid id, List<CartItem> items);
}