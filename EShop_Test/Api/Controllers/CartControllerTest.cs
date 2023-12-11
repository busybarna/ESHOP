using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using AutoFixture;
using EShop.Application.IServices;
using EShop.Controllers;
using EShop.Application.Dto;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace EShop_Test.Api.Controllers;

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
    [Fact]
    public async Task GetCartList_ReturnsOkResult()
    {
        // Arrange
        int customerId = 1; 

        var cartServiceMock = new Mock<ICartService>();
        cartServiceMock.Setup(x => x.GetCart(It.IsAny<int>()))
                       .ReturnsAsync(new List<CartItem>());

        var cartController = new CartController(cartServiceMock.Object,_logger);

        // Act
        var actionResult = await cartController.GetCartList(customerId);

        // Assert
        Assert.IsType<OkObjectResult>(actionResult);

        var okObjectResult = actionResult as OkObjectResult;
        Assert.NotNull(okObjectResult);

        var result = okObjectResult.Value as List<CartItem>;
        Assert.NotNull(result);
    }
}
