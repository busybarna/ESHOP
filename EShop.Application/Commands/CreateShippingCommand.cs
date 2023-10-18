using MediatR;
using EShop.Core.Entities;
using EShop.Core.Interfaces;

namespace EShop.Application.Commands{

    public class CreateShippingCommand: IRequest<bool>
    {
        public ShippingEntity payLoad {get;set;}
    }
     public class CreateShippingCommandHandler : IRequestHandler<CreateShippingCommand,bool>
    {
         private readonly IShippingRepository _shippingRepository;
        public CreateShippingCommandHandler(IShippingRepository shippingRepository){
            _shippingRepository = shippingRepository;
        }
        public async Task<bool> Handle(CreateShippingCommand command,CancellationToken cancellationToken)
        {
            var result = await _shippingRepository.CreateShipping(command.payLoad);
            return result;

        }
    }
}