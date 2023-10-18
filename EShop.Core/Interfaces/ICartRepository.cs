using EShop.Core.Entities;

namespace EShop.Core.Interfaces
{
    public interface ICartRepository
    {
        Task<bool> CreateCart(CartEntity cartEntity);
        Task<List<CartItemEntity>> GetCart(int CustomerId);
    }
}