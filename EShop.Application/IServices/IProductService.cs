using EShop.Application.Dto;

namespace EShop.Application.IServices
{
    public interface IProductService
    {
        Task<List<Product>> GetProductList();
        Task<bool> CreateProduct(Product product);
        
    }
}