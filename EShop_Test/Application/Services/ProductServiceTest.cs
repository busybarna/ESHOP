using AutoMapper;
using MediatR;
using NSubstitute;
using Xunit;
using AutoFixture;
using Moq;
using EShop.Application.Commands;
using EShop.Application.Queries;
using EShop.Application.Services;
using EShop.Application.Dto;
using EShop.Core.Entities;

namespace EShop_Test.Application.Services;

public class ProductServiceTest
{
    [Fact]
    public async Task CreateProduct_ShouldReturnTrue_WhenProductIsCreatedSuccessfully()
    {
        // Arrange
        var fixture = new Fixture();
        var product = fixture.Create<Product>();
        var productEntity = fixture.Create<ProductEntity>();

        var mapperMock = new Mock<IMapper>();
        var mediatorMock = new Mock<IMediator>();

        mapperMock.Setup(m => m.Map<ProductEntity>(product)).Returns(productEntity);
        mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default(CancellationToken)))
                   .ReturnsAsync(true);

        var productService = new ProductService(mediatorMock.Object, mapperMock.Object);

        // Act
        var result = await productService.CreateProduct(product);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetProductList_Empty()
    {
        // Arrange
        var expectedResult = new List<ProductEntity>
        {
            new ProductEntity {  ProductId = 1, Name = "Tv",Price = 1000},
            new ProductEntity {  ProductId = 2, Name = "Mobile",Price = 1000}
        };

        var mapperMock = new Mock<IMapper>();
        var mediatorMock = new Mock<IMediator>();

        mediatorMock.Setup(m => m.Send(It.IsAny<ProductQuery>(), default(CancellationToken)))
                   .ReturnsAsync(expectedResult);

        var productService = new ProductService(mediatorMock.Object,mapperMock.Object);

        // Act
        var result = await productService.GetProductList();

        // Assert
        Assert.Null(result);
    }
}