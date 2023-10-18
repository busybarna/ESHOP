using EShop.Core.Entities;

namespace EShop.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetProductList();
        Task<bool> CreatekProduct(ProductEntity customerEntity);
    }
}
