using Xunit;
using AutoFixture;
using EShop.Application.Dto;
using MediatR;
using EShop.Application.Queries;
using AutoMapper;
using NSubstitute;
using EShop.Application.Services;
using EShop.Core.Entities;
using EShop.Application.Commands;

public class CartServiceTest
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public CartServiceTest()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task GetCartList_Empty()
    {
        // Arrange
        int CustomerId = 1;
        
        var cartItems = new List<CartItemEntity> {
            new CartItemEntity {ProductId = 1, Quantity = 1},
            new CartItemEntity {ProductId = 2, Quantity = 1}
        };

        var query = new CartQuery(CustomerId);
        _mediator.Send(query).Returns(cartItems);
        _mapper.Map<List<CartItemEntity>>(_mediator.Send(query).Returns(cartItems));
        var res = new CartService(_mediator, _mapper);

        // Act
        var resl = await res.GetCart(CustomerId);

        // Assert
        Assert.Null(resl);
    }
    [Fact]
    public async Task CreateCart_NotNullTest()
    {
        // Arrange
        var fixture = new Fixture();
        var cart = fixture.Create<Cart>();
        var cartEntity = fixture.Create<CartEntity>();

        var query = new CreateCartCommand { payLoad = cartEntity };
        _mediator.Send(query).Returns(true);
        _mapper.Map<bool>(_mediator.Send(query).Returns(true));
        var res = new CartService(_mediator, _mapper);

        // Act
        var resl = await res.CreateCart(cart);

        // Assert
        Assert.NotNull(resl);
    }
}

