using MediatR;
using AutoMapper;
using EShop.Application.IServices;
using EShop.Application.Commands;
using EShop.Application.Queries;
using EShop.Application.Dto;
using EShop.Core.Entities;
using System;

namespace EShop.Application.Services
{

    public class CartService : ICartService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CartService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<bool> CreateCart(Cart cart)
        {
            var result = false;

            var isCustomerExists = await _mediator.Send(new GetCustomerByIdQuery(cart.CustomerId));

            if (isCustomerExists)
            {
                CartEntity cartEntity = new CartEntity();

                cartEntity.CustomerId = cart.CustomerId;

                var item = new List<CartItemEntity>();
                foreach (var product in cart.ProductItems)
                {
                    item.Add(new CartItemEntity { ProductId = product.ProductId, Quantity = product.Quantity });
                }
                cartEntity.Items = item;

                var query = new CreateCartCommand() { payLoad = cartEntity };
                result = await _mediator.Send(query);
            }

            return result;
        }
        public async Task<List<CartItem>> GetCart(int CustomerId)
        {
            List<CartItem> cartItems = new List<CartItem>();
            var query = new CartQuery(CustomerId);
            var cart = await _mediator.Send(query);
            cartItems = _mapper.Map<List<CartItem>>(cart);
            return cartItems;
        }
    }
}