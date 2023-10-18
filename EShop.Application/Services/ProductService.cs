using MediatR;
using AutoMapper;
using EShop.Application.Commands;
using EShop.Application.Queries;
using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Application.IServices;

namespace EShop.Application.Services{

    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ProductService(IMediator mediator,IMapper mapper){
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<bool> CreateProduct(Product product)
        {
            ProductEntity productEntity = new ProductEntity();
            productEntity = _mapper.Map<ProductEntity>(product);
            var query = new CreateProductCommand() {payLoad = productEntity};
            var result = await _mediator.Send(query);
            return  result;
        }

        public async Task<List<Product>> GetProductList()
        {
            List<Product> products = new List<Product>();
            var result = await _mediator.Send(new ProductQuery());
            products = _mapper.Map<List<Product>>(result);
            return products;
        } 
    }
}