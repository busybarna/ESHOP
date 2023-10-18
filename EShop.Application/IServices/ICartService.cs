using EShop.Application.Dto;

namespace EShop.Application.IServices
{
    public interface ICartService
    {
        Task<bool> CreateCart(Cart cart);
        Task<List<CartItem>> GetCart(int CustomerId);
    }
}