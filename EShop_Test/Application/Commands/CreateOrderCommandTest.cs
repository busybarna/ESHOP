using EShop.Application.Commands;
using EShop.Core.Interfaces;
using Moq;
using Xunit;

namespace EShop_Test.Application.Commands;
public class CreateOrderCommandTest
{
    [Fact]
    public async Task Handle_ReturnsTrueOnSuccessfulCreation()
    {
        // Arrange
        int CustomerId = 1;
        var mockOrderRepository = new Mock<IOrderRepository>();
        var handler = new CreateOrderCommandHandler(mockOrderRepository.Object);

        mockOrderRepository.Setup(repo => repo.CreateOrder(It.IsAny<int>()))
            .ReturnsAsync(true);

        var command = new CreateOrderCommand(CustomerId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Handle_ReturnsFalseOnFailedCreation()
    {
        // Arrange
        int CustomerId = 1;
        var mockOrderRepository = new Mock<IOrderRepository>();
        var handler = new CreateOrderCommandHandler(mockOrderRepository.Object);

        mockOrderRepository.Setup(repo => repo.CreateOrder(It.IsAny<int>()))
            .ReturnsAsync(false);

        var command = new CreateOrderCommand(CustomerId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}
