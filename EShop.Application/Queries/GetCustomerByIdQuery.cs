using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using MediatR;

namespace EShop.Application.Queries
{
    public class GetCustomerByIdQuery :IRequest<bool>
    {
        public int CustomerId { get; }
        public GetCustomerByIdQuery(int cId)
        {
            CustomerId = cId; 
        }
    }
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery,bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository){
            _customerRepository = customerRepository;
        }
        public async Task<bool> Handle(GetCustomerByIdQuery request,CancellationToken cancellationToken)
        {
           var result = await _customerRepository.GetCustomerById(request.CustomerId);
           return result;
        }
    }
}