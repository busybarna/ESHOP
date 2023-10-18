
using EShop.Application.Dto;

namespace EShop.Application.IServices
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(int CustomerId);
        Task<CustomerOrder> GetCustomerOrder(int CustomerId);
    }
}