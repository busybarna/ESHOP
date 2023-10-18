using MediatR;
using EShop.Core.Models;
using EShop.Application.Dto;
using EShop.Core.Interfaces;
using EShop.Core.Entities;

namespace EShop.Application.Commands{

    public class CreateCartCommand: IRequest<bool>
    {
        public CartEntity payLoad {get;set;}
    }
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand,bool>
    {
        private readonly ICartRepository _cartRepository;
        public CreateCartCommandHandler(ICartRepository cartRepository){
            _cartRepository = cartRepository;
        }
        public async Task<bool> Handle(CreateCartCommand command,CancellationToken cancellationToken)
        {

            var result = await _cartRepository.CreateCart(command.payLoad);
            return result;
        }
    }

}