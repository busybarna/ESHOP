using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Entities
{
    public class CartItemEntity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
