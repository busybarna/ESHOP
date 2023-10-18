using Xunit;
using Moq;
using AutoFixture;
using AutoMapper;
using System.Data;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using EShop.Core.Entities;
using EShop.Infrastructure.Repositories;

public class ShippingRepositoryTest
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
    public async Task GetShippingList_ShouldReturnListOfOrders()
    {
        // Arrange
        var dapperMock = new Mock<IDapper>();
        var mapperMock = new Mock<IMapper>();
        int CustomerId = 1;

        var shippingData = new List<ShippingDataModel>
        {
            new ShippingDataModel { CustomerId = 1, Address1 = "Chennai", Address2 = "Tamil Nadu" }
        };

        var expectedShippingEntities = new List<ShippingEntity>
        {
           new ShippingEntity { CustomerId = 1, Address1 = "Chennai", Address2 = "Tamil Nadu" }
        };

        dapperMock.Setup(d => d.GetAll<ShippingDataModel>(It.IsAny<string>(), It.IsAny<ShippingDataModel>(), It.IsAny<CommandType>()))
                  .Returns(shippingData);

        mapperMock.Setup(m => m.Map<List<ShippingEntity>>(shippingData))
                  .Returns(expectedShippingEntities);

        var shippingRepo = new ShippingRepository(dapperMock.Object, mapperMock.Object);

        // Act
        var result = await shippingRepo.GetShippingList(CustomerId);

        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public async Task CreateShipping_WithValidData()
    {
        // Arrange
        var fixture = new Fixture();
        var shippingEntity = fixture.Create<ShippingEntity>();

        var mapper = new Mock<IMapper>();

        var dapper = new Mock<IDapper>();
        dapper.Setup(d => d.Insert<ShippingEntity>(It.IsAny<string>(), It.IsAny<ShippingEntity>(), It.IsAny<CommandType>()))
              .Returns(shippingEntity);

        var shippingRepo = new ShippingRepository(dapper.Object, mapper.Object);

        // Act
        var result = await shippingRepo.CreateShipping(shippingEntity);

        // Assert
        Assert.True(result);
    }
}
