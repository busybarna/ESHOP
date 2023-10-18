namespace EShop.Core.Entities
{
    public class CartEntity
    {
         public int CustomerId { get; set; }
        public List<CartItemEntity> Items { get; set; }
    }
}