using MediatR;
using EShop.Core.Entities;
using EShop.Core.Interfaces;

namespace EShop.Application.Queries
{

    public class ShippingQuery : IRequest<List<ShippingEntity>>
    {
        public int CustomerId { get; }
        public ShippingQuery(int cId)
        {
            CustomerId = cId;
        }
    }
    public class GetShippingQueryHandler : IRequestHandler<ShippingQuery, List<ShippingEntity>>
    {
        private readonly IShippingRepository _shippingRepository;
        public GetShippingQueryHandler(IShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }
        public async Task<List<ShippingEntity>> Handle(ShippingQuery command, CancellationToken cancellationToken)
        {
            var shippingEntities = await _shippingRepository.GetShippingList(command.CustomerId);
            return shippingEntities;
        }
    }
}