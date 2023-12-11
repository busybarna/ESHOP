using EShop.Application.Dto;

namespace EShop.Application.IServices
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomerList();
        Task<bool> CreateCustomer(Customer customer);
    }
}