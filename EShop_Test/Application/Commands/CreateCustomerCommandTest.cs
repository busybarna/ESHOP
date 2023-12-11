using EShop.Application.Commands;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using Moq;
using Xunit;
using AutoFixture;

namespace EShop_Test.Application.Commands;
public class CreateCustomerCommandTest
{
    [Fact]
    public async Task Handle_ReturnsTrueOnSuccessfulCreation()
    {
        // Arrange
        var fixture = new Fixture();
        var customerEntity = fixture.Create<CustomerEntity>();

        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var handler = new CreateCustomerCommandHandler(mockCustomerRepository.Object);

        mockCustomerRepository.Setup(repo => repo.CreateCustomer(It.IsAny<CustomerEntity>()))
            .ReturnsAsync(true);

        var command = new CreateCustomerCommand() { payLoad = customerEntity };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Handle_ReturnsFalseOnFailedCreation()
    {
        // Arrange
        var fixture = new Fixture();
        var productEntity = fixture.Create<CustomerEntity>();

        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var handler = new CreateCustomerCommandHandler(mockCustomerRepository.Object);

        mockCustomerRepository.Setup(repo => repo.CreateCustomer(It.IsAny<CustomerEntity>()))
            .ReturnsAsync(false);

        var command = new CreateCustomerCommand() { payLoad = productEntity };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}
