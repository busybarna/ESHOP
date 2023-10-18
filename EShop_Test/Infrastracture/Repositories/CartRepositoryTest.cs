using Xunit;
using Moq;
using AutoFixture;
using AutoMapper;
using System.Data;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using EShop.Core.Entities;
using EShop.Infrastructure.Repositories;

public class CartRepositoryTest
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenDapperIsNull()
    {
        // Arrange
        IDapper dapper = null;
        IMapper mapper = new Mock<IMapper>().Object;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CartRepository(dapper, mapper));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenMapperIsNull()
    {
        // Arrange
        IDapper dapper = new Mock<IDapper>().Object;
        IMapper mapper = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CartRepository(dapper, mapper));
    }

    [Fact]
    public void Constructor_ShouldNotThrow_WhenDapperAndMapperAreNotNull()
    {
        // Arrange
        IDapper dapper = new Mock<IDapper>().Object;
        IMapper mapper = new Mock<IMapper>().Object;

        // Act & Assert
        Assert.NotNull(new CartRepository(dapper, mapper));
    }
    [Fact]
    public async Task GetOrderList_ShouldReturnListOfOrders()
    {
        // Arrange
        var dapperMock = new Mock<IDapper>();
        var mapperMock = new Mock<IMapper>();
        int CustomerId = 1;

        var cartData = new List<CartItemDataModel>
        {
            new CartItemDataModel { CustomerId = 1, ProductId = 1, Quantity = 1 },
            new CartItemDataModel { CustomerId = 2, ProductId = 2, Quantity = 1}
        };

        var expectedCartEntities = new List<CartItemEntity>
        {
           new CartItemEntity { ProductId = 1, Quantity = 1 },
           new CartItemEntity { ProductId = 2,  Quantity = 1 }
        };

        dapperMock.Setup(d => d.GetAll<CartItemDataModel>(It.IsAny<string>(), It.IsAny<CartItemDataModel>(), It.IsAny<CommandType>()))
                  .Returns(cartData);

        mapperMock.Setup(m => m.Map<List<CartItemEntity>>(cartData))
                  .Returns(expectedCartEntities);

        var cartRepo = new CartRepository(dapperMock.Object, mapperMock.Object);

        // Act
        var result = await cartRepo.GetCart(CustomerId);

        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public async Task CreateOrder_WithValidData()
    {
        // Arrange
        var fixture = new Fixture();
        var cartEntity = fixture.Create<CartEntity>();

        var mapper = new Mock<IMapper>();

        var dapper = new Mock<IDapper>();
        dapper.Setup(d => d.Insert<CartEntity>(It.IsAny<string>(), It.IsAny<CartEntity>(), It.IsAny<CommandType>()))
              .Returns(cartEntity);

        var cartRepo = new CartRepository(dapper.Object, mapper.Object);

        // Act
        var result = await cartRepo.CreateCart(cartEntity);

        // Assert
        Assert.True(result);
    }
}
