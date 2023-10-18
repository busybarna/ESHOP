using Xunit;
using Moq;
using AutoFixture;
using AutoMapper;
using System.Data;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using EShop.Core.Entities;
using EShop.Infrastructure.Repositories;

public class ProductRepositoryTest
{
    [Fact]
    public async Task GetProductList_ShouldReturnListOfProducts()
    {
        // Arrange
        var dapperMock = new Mock<IDapper>();
        var mapperMock = new Mock<IMapper>();

        var productData = new List<ProductDataModel>
        {
            new ProductDataModel { ProductId = 1, Name = "Test", Price = 100 },
            new ProductDataModel { ProductId = 1, Name = "Test", Price = 100 }
        };

        var expectedProductEntities = new List<ProductEntity>
        {
            new ProductEntity { ProductId = 1, Name = "Test", Price = 100 },
            new ProductEntity { ProductId = 1, Name = "Test", Price = 100  }
        };

        dapperMock.Setup(d => d.GetAll<ProductDataModel>(It.IsAny<string>(), It.IsAny<ProductDataModel>(), It.IsAny<CommandType>()))
                  .Returns(productData);

        mapperMock.Setup(m => m.Map<List<ProductEntity>>(productData))
                  .Returns(expectedProductEntities);

        var productRepo = new ProductRepository(dapperMock.Object, mapperMock.Object);

        // Act
        var result = await productRepo.GetProductList();

        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public async Task CreateProduct_WithValidData()
    {
        // Arrange
        var fixture = new Fixture();
        var productEntity = fixture.Create<ProductEntity>();
        var productDataModel = fixture.Create<ProductDataModel>();

        var mapper = new Mock<IMapper>();
        mapper.Setup(m => m.Map<ProductDataModel>(It.IsAny<ProductEntity>()))
              .Returns(productDataModel);

        var dapper = new Mock<IDapper>();
        dapper.Setup(d => d.Insert<ProductDataModel>(It.IsAny<string>(), It.IsAny<ProductDataModel>(), It.IsAny<CommandType>()))
              .Returns(productDataModel);

        var productRepo = new ProductRepository(dapper.Object, mapper.Object);

        // Act
        var result = await productRepo.CreatekProduct(productEntity);

        // Assert
        Assert.True(result);
    }
}
