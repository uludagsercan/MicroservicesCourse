
namespace Microservice.Basket.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = default!;

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public ShoppingCart()
        {
            
        }

        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
        

    }
}