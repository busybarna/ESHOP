using MediatR;
using EShop.Core.Entities;
using EShop.Core.Interfaces;

namespace EShop.Application.Queries
{

    public class ProductQuery : IRequest<List<ProductEntity>>
    {
    }
    public class GetProductListQueryHandler : IRequestHandler<ProductQuery, List<ProductEntity>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductListQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductEntity>> Handle(ProductQuery command, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductList();
            return products;
        }
    }
}