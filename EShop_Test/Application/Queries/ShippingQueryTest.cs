using Xunit;
using Moq;
using EShop.Core.Interfaces;
using EShop.Core.Entities;
using EShop.Application.Queries;

namespace EShop_Test.Application.Queries;
public class ShippingQueryTest
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldReturnListOfShippingData()
    {
        // Arrange
        int CustomerId = 1;
        var shippingRepository = new Mock<IShippingRepository>();
        var shippingsToReturn = new List<ShippingEntity>
        {
            new ShippingEntity { CustomerId = 1, Address1 = "Chennai", Address2 = "Tamil Nadu" }
        };
        shippingRepository.Setup(repo => repo.GetShippingList(CustomerId))
                        .ReturnsAsync(shippingsToReturn);

        var query = new ShippingQuery(CustomerId); 

        var handler = new GetShippingQueryHandler(shippingRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
       
    }
}
