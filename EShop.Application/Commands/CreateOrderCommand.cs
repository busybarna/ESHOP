using MediatR;
using EShop.Core.Interfaces;

namespace EShop.Application.Commands
{

    public class CreateOrderCommand : IRequest<bool>
    {
        public int CustomerId { get; }
        public CreateOrderCommand(int cId)
        {
            CustomerId = cId;
        }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<bool> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {

            var result = await _orderRepository.CreateOrder(command.CustomerId);
            return result;

        }
    }
}