using MediatR;
using EShop.Core.Entities;
using EShop.Core.Interfaces;

namespace EShop.Application.Commands{

    public class CreateProductCommand: IRequest<bool>
    {
        public ProductEntity payLoad {get;set;}
    }
     public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,bool>
    {
         private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository){
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(CreateProductCommand command,CancellationToken cancellationToken)
        {
            var result = await _productRepository.CreatekProduct(command.payLoad);
            return result;

        }
    }
}