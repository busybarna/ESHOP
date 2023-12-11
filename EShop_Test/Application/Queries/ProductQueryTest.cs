using Xunit;
using Moq;
using EShop.Core.Interfaces;
using EShop.Core.Entities;
using EShop.Application.Queries;

namespace EShop_Test.Application.Queries;
public class ProductQueryTests
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldReturnListOfProducts()
    {
        // Arrange
        var productRepository = new Mock<IProductRepository>();
        var productsToReturn = new List<ProductEntity>
        {
            new ProductEntity { ProductId = 1, Name = "Test", Price = 1000 },
            new ProductEntity { ProductId = 2, Name = "Test Name", Price = 2000 }
        };
        productRepository.Setup(repo => repo.GetProductList())
                        .ReturnsAsync(productsToReturn);

        var query = new ProductQuery(); 

        var handler = new GetProductListQueryHandler(productRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
       
    }
}
