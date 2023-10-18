using Xunit;
using Moq;
using AutoFixture;
using AutoMapper;
using System.Data;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using EShop.Core.Entities;
using EShop.Infrastructure.Repositories;

public class OrderRepositoryTest
{
    [Fact]
    public async Task GetOrderList_ShouldReturnListOfOrders()
    {
        // Arrange
        var dapperMock = new Mock<IDapper>();
        var mapperMock = new Mock<IMapper>();
        int CustomerId = 1;

        var orderData = new List<OrderDataModel>
        {
            new OrderDataModel { CustomerId = 1, CustomerNumber = "12345", OrderNumber = "23456", Quantity = 1, NetAmount = 10 },
            new OrderDataModel { CustomerId = 2, CustomerNumber = "54321", OrderNumber = "67898", Quantity = 1, NetAmount = 10 }
        };

        var expectedOrderEntities = new List<OrderEntity>
        {
           new OrderEntity { CustomerId = 1, CustomerNumber = "12345", OrderNumber = "23456", Quantity = 1, NetAmount = 10 },
            new OrderEntity { CustomerId = 2, CustomerNumber = "54321", OrderNumber = "67898", Quantity = 1, NetAmount = 10 }
        };

        dapperMock.Setup(d => d.GetAll<OrderDataModel>(It.IsAny<string>(), It.IsAny<OrderDataModel>(), It.IsAny<CommandType>()))
                  .Returns(orderData);

        mapperMock.Setup(m => m.Map<List<OrderEntity>>(orderData))
                  .Returns(expectedOrderEntities);

        var orderRepo = new OrderRepository(dapperMock.Object, mapperMock.Object);

        // Act
        var result = await orderRepo.GetCustomerOrder(CustomerId);

        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public async Task CreateOrder_WithValidData()
    {
        // Arrange
        var fixture = new Fixture();
        var orderDataModel = fixture.Create<OrderDataModel>();
        int CustomerId = 1;

        var mapper = new Mock<IMapper>();

        var dapper = new Mock<IDapper>();
        dapper.Setup(d => d.Insert<OrderDataModel>(It.IsAny<string>(), It.IsAny<OrderDataModel>(), It.IsAny<CommandType>()))
              .Returns(orderDataModel);

        var orderRepo = new OrderRepository(dapper.Object, mapper.Object);

        // Act
        var result = await orderRepo.CreateOrder(CustomerId);

        // Assert
        Assert.True(result);
    }
}
