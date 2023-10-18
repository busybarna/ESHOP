using MediatR;
using EShop.Core.Models;
using EShop.Core.Interfaces;
using EShop.Core.Entities;

namespace EShop.Application.Commands{

    public class CreateCustomerCommand: IRequest<bool>
    {
        public CustomerEntity payLoad {get;set;}

    }
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand,bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository){
            _customerRepository = customerRepository;
        }
        public async Task<bool> Handle(CreateCustomerCommand command,CancellationToken cancellationToken){

            var result = await _customerRepository.CreateCustomer(command.payLoad);
            return result;
        }
    }
}