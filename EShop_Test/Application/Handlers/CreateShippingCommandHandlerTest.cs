using EShop.Application.Commands;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using Moq;
using Xunit;
using AutoFixture;

public class CreateShippingCommandHandlerTest
{
    [Fact]
    public async Task Handle_ReturnsTrueOnSuccessfulCreation()
    {
        // Arrange
        var fixture = new Fixture();
        var shippingEntity = fixture.Create<ShippingEntity>();

        var mockShippingRepository = new Mock<IShippingRepository>();
        var handler = new CreateShippingCommandHandler(mockShippingRepository.Object);

        mockShippingRepository.Setup(repo => repo.CreateShipping(It.IsAny<ShippingEntity>()))
            .ReturnsAsync(true);

        var command = new CreateShippingCommand() { payLoad = shippingEntity };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Handle_ReturnsFalseOnFailedCreation()
    {
        // Arrange
        var fixture = new Fixture();
        var shippingEntity = fixture.Create<ShippingEntity>();

        var mockShippingRepository = new Mock<IShippingRepository>();
        var handler = new CreateShippingCommandHandler(mockShippingRepository.Object);

        mockShippingRepository.Setup(repo => repo.CreateShipping(It.IsAny<ShippingEntity>()))
            .ReturnsAsync(false);

        var command = new CreateShippingCommand() { payLoad = shippingEntity };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}
