using EShop.Application.Commands;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using Moq;
using Xunit;
using AutoFixture;

namespace EShop_Test.Application.Commands;
public class CreateProductCommandTest
{
    [Fact]
    public async Task Handle_ReturnsTrueOnSuccessfulCreation()
    {
        // Arrange
        var fixture = new Fixture();
        var productEntity = fixture.Create<ProductEntity>();

        var mockProductRepository = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(mockProductRepository.Object);

        mockProductRepository.Setup(repo => repo.CreatekProduct(It.IsAny<ProductEntity>()))
            .ReturnsAsync(true);

        var command = new CreateProductCommand() { payLoad = productEntity };

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
        var productEntity = fixture.Create<ProductEntity>();

        var mockProductRepository = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(mockProductRepository.Object);

        mockProductRepository.Setup(repo => repo.CreatekProduct(It.IsAny<ProductEntity>()))
            .ReturnsAsync(false);

        var command = new CreateProductCommand() { payLoad = productEntity };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}
