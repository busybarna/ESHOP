using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using AutoFixture;
using EShop.Application.IServices;
using EShop.Controllers;
using EShop.Application.Dto;

namespace EShop_Test;

public class ProductControllerTest
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;
    public ProductControllerTest()
    {
        _productService = Substitute.For<IProductService>();
        _logger = Substitute.For<ILogger<ProductController>>();
    }

    [Fact]
    public async Task GetProductList_ShouldReturnValue()
    {
        var products = new List<Product>
        {
            new Product { ProductId = 1, Name = "Test", Price = 1000 },
            new Product { ProductId = 2, Name = "Test Name", Price = 2000 }
        };

        _productService.GetProductList().Returns(products);
        var mainresult = new ProductController(_productService, _logger);
        var result = await mainresult.GetProductList();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task CreateProductController_ShouldReturnValue()
    {
        var fixture = new Fixture();
        var product = fixture.Create<Product>();

        _productService.CreateProduct(product).Returns(true);
        var mainresult = new ProductController(_productService, _logger);
        var result = await mainresult.CreateProduct(product);
        Assert.NotNull(result);
    }
}
