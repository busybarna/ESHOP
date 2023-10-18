using Xunit;
using Moq;
using EShop.Core.Interfaces;
using EShop.Core.Entities;
using EShop.Application.Queries;

public class GetCartQueryHandlerTest
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldReturnListOfCarts()
    {
        // Arrange
        int CustomerId = 1;
        var cartRepository = new Mock<ICartRepository>();
        var cartsToReturn = new List<CartItemEntity>
        {
            new CartItemEntity { ProductId = 1, Quantity = 1 },
            new CartItemEntity { ProductId = 2, Quantity = 2 }
        };
        cartRepository.Setup(repo => repo.GetCart(CustomerId))
                        .ReturnsAsync(cartsToReturn);

        var query = new CartQuery(CustomerId); 

        var handler = new GetCartQueryHandler(cartRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
       
    }
}
