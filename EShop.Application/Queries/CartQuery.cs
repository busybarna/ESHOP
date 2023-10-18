using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using MediatR;

namespace EShop.Application.Queries
{
    public class CartQuery :IRequest<List<CartItemEntity>>
    {
        public int CustomerId { get; }
        public CartQuery(int cId)
        {
            CustomerId = cId; 
        }
    }
    public class GetCartQueryHandler : IRequestHandler<CartQuery,List<CartItemEntity>>
    {
        private readonly ICartRepository _cartRepository;
        public GetCartQueryHandler(ICartRepository cartRepository){
            _cartRepository = cartRepository;
        }
        public async Task<List<CartItemEntity>> Handle(CartQuery request,CancellationToken cancellationToken)
        {
           var result = await _cartRepository.GetCart(request.CustomerId);
           return result;
        }
    }
}