using EShop.Application.Dto;

namespace EShop.Application.IServices
{
    public interface IShippingService
    {
        Task<List<Shipping>> GetShippingData(int CustomerId);
        Task<bool> CreateShippingData(Shipping shipping);
        
    }
}