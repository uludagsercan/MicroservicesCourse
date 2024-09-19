

namespace Microservice.Basket.Models
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; set;}
        public string Color { get; set; } = default!;
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } =default!;
    }
}