namespace EShop.Application.Dto
{   
    public class Cart
    {
        public int CustomerId { get; set; }
        public List<CartItem> ProductItems { get; set; }

    }
}