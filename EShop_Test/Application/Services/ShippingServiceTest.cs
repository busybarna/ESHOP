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

namespace EShop_Test.Application.Services;

public class ShippingServiceTest
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public ShippingServiceTest()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task GetCartList_Empty()
    {
        // Arrange
        int CustomerId = 1;
        
        var shippingEntities = new List<ShippingEntity> {
            new ShippingEntity {CustomerId = 1, Address1 = "Chennai", Address2 = "Tamil Nadu"}
        };

        var query = new ShippingQuery(CustomerId);
        _mediator.Send(query).Returns(shippingEntities);
        _mapper.Map<List<ShippingEntity>>(_mediator.Send(query).Returns(shippingEntities));
        var res = new ShippingService(_mediator, _mapper);

        // Act
        var resl = await res.GetShippingData(CustomerId);

        // Assert
        Assert.Null(resl);
    }
    [Fact]
    public async Task CreateShipping_NotNullTest()
    {
        // Arrange
        var fixture = new Fixture();
        var shipping = fixture.Create<Shipping>();
        var shippingEntity = fixture.Create<ShippingEntity>();

        var query = new CreateShippingCommand { payLoad = shippingEntity };
        _mediator.Send(query).Returns(true);
        _mapper.Map<bool>(_mediator.Send(query).Returns(true));
        var res = new ShippingService(_mediator, _mapper);

        // Act
        var resl = await res.CreateShippingData(shipping);

        // Assert
        Assert.NotNull(resl);
    }
}

