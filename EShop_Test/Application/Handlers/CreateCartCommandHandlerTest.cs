using EShop.Application.Commands;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using Moq;
using Xunit;
using AutoFixture;

public class CreateCartCommandHandlerTest
{
    [Fact]
    public async Task Handle_ReturnsTrueOnSuccessfulCreation()
    {
        // Arrange
        var fixture = new Fixture();
        var cartEntity = fixture.Create<CartEntity>();

        var mockCartRepository = new Mock<ICartRepository>();
        var handler = new CreateCartCommandHandler(mockCartRepository.Object);

        mockCartRepository.Setup(repo => repo.CreateCart(It.IsAny<CartEntity>()))
            .ReturnsAsync(true);

        var command = new CreateCartCommand() { payLoad = cartEntity };

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
        var cartEntity = fixture.Create<CartEntity>();

        var mockCartRepository = new Mock<ICartRepository>();
        var handler = new CreateCartCommandHandler(mockCartRepository.Object);

        mockCartRepository.Setup(repo => repo.CreateCart(It.IsAny<CartEntity>()))
            .ReturnsAsync(false);

        var command = new CreateCartCommand() { payLoad = cartEntity };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}
