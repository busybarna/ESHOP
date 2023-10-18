using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using MediatR;

namespace EShop.Application.Queries
{
    public class CustomerOrderQuery :IRequest<List<OrderEntity>>
    {
        public int CustomerId { get; }
        public CustomerOrderQuery(int cId)
        {
            CustomerId = cId; 
        }
    }
    public class GetCustomerOrderQueryHandler : IRequestHandler<CustomerOrderQuery,List<OrderEntity>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetCustomerOrderQueryHandler(IOrderRepository orderRepository){
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderEntity>> Handle(CustomerOrderQuery request,CancellationToken cancellationToken){
            
            var customers = await _orderRepository.GetCustomerOrder(request.CustomerId);
            return customers;
        }
    }
}