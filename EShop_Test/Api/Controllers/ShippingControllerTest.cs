using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using AutoFixture;
using EShop.Application.IServices;
using EShop.Controllers;
using EShop.Application.Dto;

namespace EShop_Test.Api.Controllers;

public class ShippingControllerTest
{
    private readonly IShippingService _shippingService;
    private readonly ILogger<ShippingController> _logger;
    public ShippingControllerTest()
    {
        _shippingService = Substitute.For<IShippingService>();
        _logger = Substitute.For<ILogger<ShippingController>>();
    }

    [Fact]
    public async Task CreateShippingDataController_ShouldReturnValue()
    {
        var fixture = new Fixture();
        var shipping = fixture.Create<Shipping>();

        _shippingService.CreateShippingData(shipping).Returns(true);
        var mainresult = new ShippingController(_shippingService, _logger);
        var result = await mainresult.CreateShippingData(shipping);
        Assert.NotNull(result);
    }
    [Fact]
    public async Task GetShippingList_ShouldReturnValue()
    {
        int CustomerId = 1;

        var shippingList = new List<Shipping> {
            new Shipping { CustomerId = 1, Address1 = "Chennai", Address2 = "Tamil Nadu" }
        };

        _shippingService.GetShippingData(1).Returns(shippingList);
        var mainresult = new ShippingController(_shippingService, _logger);
        var result = await mainresult.GetShippingData(CustomerId);
        Assert.NotNull(result);
    }
}
