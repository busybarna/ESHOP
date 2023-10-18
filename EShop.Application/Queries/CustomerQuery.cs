using MediatR;
using EShop.Core.Interfaces;
using EShop.Core.Entities;

namespace EShop.Application.Queries{

    public class CustomerQuery: IRequest<List<CustomerEntity>>
    {
    }
    public class GetCustomerListQueryHandler : IRequestHandler<CustomerQuery,List<CustomerEntity>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerListQueryHandler(ICustomerRepository customerRepository){
            _customerRepository = customerRepository;
        }
        public async Task<List<CustomerEntity>> Handle(CustomerQuery command,CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetCustomerList();
            return customers;
        }
    }
}