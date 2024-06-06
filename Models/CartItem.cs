namespace WebApplication2.Models
{
    public class CartItem
    {
        public int Id { get; set; }  // This should match your product ID type
        public string UserId { get; set; }
        public string ProductId { get; set; }  // Should be string to match with cartItem.ProductId
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        // Optional: Calculated property for the total price
        public int TotalPrice => Quantity * int.Parse(Price);
    }
}
