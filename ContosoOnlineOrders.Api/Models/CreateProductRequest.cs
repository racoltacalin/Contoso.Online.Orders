namespace ContosoOnlineOrders.Api.Models
{
    public class CreateProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InventoryCount { get; set; }
    }
}