using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using AutoFixture;
using EShop.Application.IServices;
using EShop.Controllers;
using EShop.Application.Dto;

namespace EShop_Test;

public class CartControllerTest
{
    private readonly ICartService _cartService;
    private readonly ILogger<CartController> _logger;
    public CartControllerTest()
    {
        _cartService = Substitute.For<ICartService>();
        _logger = Substitute.For<ILogger<CartController>>();
    }

    [Fact]
    public async Task CreateCartController_ShouldReturnValue()
    {
        var fixture = new Fixture();
        var cart = fixture.Create<Cart>();

        _cartService.CreateCart(cart).Returns(true);
        var mainresult = new CartController(_cartService, _logger);
        var result = await mainresult.CreateCart(cart);
        Assert.NotNull(result);
    }
    [Fact]
    public async Task GetCartList_ShouldReturnValue()
    {
        int CustomerId = 1;

        var cartList = new List<CartItem> {
            new CartItem { ProductId = 1, Quantity = 1 },
            new CartItem { ProductId = 2, Quantity = 1}
        };

        _cartService.GetCart(1).Returns(cartList);
        var mainresult = new CartController(_cartService, _logger);
        var result = await mainresult.GetCartList(CustomerId);
        Assert.NotNull(result);
    }
}
