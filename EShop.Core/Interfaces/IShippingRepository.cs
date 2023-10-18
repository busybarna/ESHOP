using EShop.Core.Entities;

namespace EShop.Core.Interfaces
{
    public interface IShippingRepository
    {
        Task<List<ShippingEntity>> GetShippingList(int CustomerId);
        Task<bool> CreateShipping(ShippingEntity shippingEntity);
    }
}
