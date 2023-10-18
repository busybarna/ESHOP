using Xunit;
using Moq;
using EShop.Core.Interfaces;
using EShop.Core.Entities;
using EShop.Application.Queries;

public class GetCustomerListQueryHandlerTest
{
    [Fact]
    public async Task Handle_WhenCalled_ShouldReturnListOfCustomers()
    {
        // Arrange
        var customerRepository = new Mock<ICustomerRepository>();
        var customersToReturn = new List<CustomerEntity>
        {
           new CustomerEntity {CustomerId = 1, CustomerNumber = "12345",Name = "Test Customer"},
           new CustomerEntity {CustomerId = 2, CustomerNumber = "54321",Name = "Test Customer new"}
        };
        customerRepository.Setup(repo => repo.GetCustomerList())
                        .ReturnsAsync(customersToReturn);

        var query = new CustomerQuery(); 

        var handler = new GetCustomerListQueryHandler(customerRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
       
    }
}
