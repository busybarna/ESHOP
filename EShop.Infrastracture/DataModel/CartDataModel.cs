using EShop.Core.Entities;

namespace EShop.Infrastructure.DataModel
{
    public class CartDataModel
    {
        public int CustomerId { get; set; }
        public List<CartItemDataModel> Items { get; set; }
    }
}
