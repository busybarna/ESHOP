using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using EShop.Application.IServices;
using EShop.Controllers;
using EShop.Application.Dto;

namespace EShop_Test;

public class OrderControllerTest
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;
    public OrderControllerTest()
    {
        _orderService = Substitute.For<IOrderService>();
        _logger = Substitute.For<ILogger<OrderController>>();
    }

    [Fact]
    public async Task CreateOrderController_ShouldReturnValue()
    {
        int CustomerId = 1;
        _orderService.CreateOrder(1).Returns(true);
        var mainresult = new OrderController(_orderService, _logger);
        var result = await mainresult.CreateOrder(CustomerId);
        Assert.NotNull(result);
    }
    [Fact]
    public async Task GetCustomerOrder_ShouldReturnValue()
    {
        var item = new List<string> {
            "milk",
            "ball"
        };

        var Shipping = new List<string> {
            "Chennai",
            "India"
         };

        var customerOrder = new CustomerOrder
        {
            CustomerNumber = "4567890",
            OrderNumber = "3344656",
            Total = 100,
            itemlist = item,
            Shipping = Shipping
        };

        int CustomerId = 1;
        _orderService.GetCustomerOrder(CustomerId).Returns(customerOrder);
        var mainresult = new OrderController(_orderService, _logger);
        var result = await mainresult.GetCustomerOrder(CustomerId);
        Assert.NotNull(result);
    }
}
