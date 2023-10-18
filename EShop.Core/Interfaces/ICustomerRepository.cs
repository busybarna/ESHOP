using EShop.Core.Entities;

namespace EShop.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerEntity>> GetCustomerList();
        Task<bool> CreateCustomer(CustomerEntity customerEntity);

        Task<bool> GetCustomerById(int CustomerId);
    }
}
