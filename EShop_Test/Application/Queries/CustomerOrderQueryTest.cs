using Xunit;
using Moq;
using EShop.Core.Interfaces;
using EShop.Core.Entities;
using EShop.Application.Queries;

namespace EShop_Test.Application.Queries;
public class CustomerOrderQueryTest
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldReturnListOfOrders()
    {
        // Arrange
        int customerId = 1;
        var orderRepository = new Mock<IOrderRepository>();
        var ordersToReturn = new List<OrderEntity>
        {
           new OrderEntity {
                CustomerId = customerId, 
                CustomerNumber = "12345",
                OrderNumber = "67890",
                IsLoyaltyMembership = true,
                address1 = "Address1",
                address2 = "Address2",
                NetAmount = 50, 
                ProductName = "Product1",
                Quantity = 2 },

           new OrderEntity {
                CustomerId = customerId, 
                CustomerNumber = "12345",
                OrderNumber = "67890",
                IsLoyaltyMembership = true,
                address1 = "Address1",
                address2 = "Address2",
                NetAmount = 500, 
                ProductName = "Product2",
                Quantity = 2 }
        };

        orderRepository.Setup(repo => repo.GetCustomerOrder(customerId))
                        .ReturnsAsync(ordersToReturn);

        var query = new CustomerOrderQuery(customerId); 

        var handler = new GetCustomerOrderQueryHandler(orderRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
       
    }
}
