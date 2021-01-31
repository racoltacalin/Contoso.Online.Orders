namespace ContosoOnlineOrders.Api.Models
{
    public record InventoryUpdateRequest(int productId, int countToAdd);
}