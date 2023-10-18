using EShop.Core.Entities;

namespace EShop.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrderEntity>> GetCustomerOrder(int CustomerId);
        Task<bool> CreateOrder(int CustomerId);
    }
}
