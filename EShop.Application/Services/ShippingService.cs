using MediatR;
using AutoMapper;
using EShop.Application.Commands;
using EShop.Application.Queries;
using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Application.IServices;

namespace EShop.Application.Services{

    public class ShippingService : IShippingService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ShippingService(IMediator mediator,IMapper mapper){
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<bool> CreateShippingData(Shipping shipping)
        {
            ShippingEntity shippingEntity = new ShippingEntity();
            shippingEntity = _mapper.Map<ShippingEntity>(shipping);
            var query = new CreateShippingCommand() {payLoad = shippingEntity};
            var result = await _mediator.Send(query);
            return  result;
        }

        public async Task<List<Shipping>> GetShippingData(int CustomerId)
        {
            List<Shipping> shippings = new List<Shipping>();
            var result = await _mediator.Send(new ShippingQuery(CustomerId));
            shippings = _mapper.Map<List<Shipping>>(result);
            return shippings;
        } 
    }
}