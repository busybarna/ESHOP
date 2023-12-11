using Xunit;
using Moq;
using MediatR;
using EShop.Application.Queries;
using AutoMapper;
using EShop.Application.Services;
using EShop.Core.Entities;
using NSubstitute;
using EShop.Application.Commands;

namespace EShop_Test.Application.Services;
public class OrderServiceTest
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public OrderServiceTest()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
    }
    [Fact]
    public async Task GetCustomerOrder_Empty()
    {
        // Arrange
        int customerId = 1;
        var customerOrderQueryResult = new List<OrderEntity>
        {
            
            new OrderEntity
            {
                CustomerId = customerId,
                CustomerNumber = "12345",
                OrderNumber = "67890",
                IsLoyaltyMembership = true,
                address1 = "Address1",
                address2 = "Address2",
                NetAmount = 50, 
                ProductName = "Product1",
                Quantity = 2 
            },
            
        };

        // Mocking _mediator
        var mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(x => x.Send(It.IsAny<CustomerOrderQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customerOrderQueryResult);

        // Mocking _mapper
        var mapperMock = new Mock<IMapper>();

        // Create an instance of OrderService 
        var orderService = new OrderService(mediatorMock.Object, mapperMock.Object);

        // Act
        var result = await orderService.GetCustomerOrder(customerId);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task CreateOrder_NotNullTest()
    {
        int customerId = 1;

        var query = new CreateOrderCommand(customerId);
        _mediator.Send(query).Returns(true);
        _mapper.Map<bool>(_mediator.Send(query).Returns(true));
        var res = new OrderService(_mediator, _mapper);
        var resl = await res.CreateOrder(customerId);

        Assert.NotNull(resl);
    }
}
