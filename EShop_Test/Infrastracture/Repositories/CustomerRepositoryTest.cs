using Xunit;
using Moq;
using AutoFixture;
using AutoMapper;
using System.Data;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using EShop.Core.Entities;
using EShop.Infrastructure.Repositories;

public class CustomerServiceTests
{
    [Fact]
    public async Task GetCustomerList_ShouldReturnListOfCustomers()
    {
        // Arrange
        var dapperMock = new Mock<IDapper>(); 
        var mapperMock = new Mock<IMapper>();

        var customerData = new List<CustomerDataModel>
        {
            new CustomerDataModel { CustomerId = 1, CustomerNumber = "12345", Name = "Test" },
            new CustomerDataModel { CustomerId = 2, CustomerNumber = "54321", Name = "Test new" }
        };

        var expectedCustomerEntities = new List<CustomerEntity>
        {
            new CustomerEntity { CustomerId = 1, CustomerNumber = "12345", Name = "Test" },
            new CustomerEntity { CustomerId = 2, CustomerNumber = "54321", Name = "Test new"  }
        };

        dapperMock.Setup(d => d.GetAll<CustomerDataModel>(It.IsAny<string>(), It.IsAny<CustomerDataModel>(), It.IsAny<CommandType>()))
                  .Returns(customerData);

        mapperMock.Setup(m => m.Map<List<CustomerEntity>>(customerData))
                  .Returns(expectedCustomerEntities);

        var customerRepo = new CustomerRepository(dapperMock.Object, mapperMock.Object);

        // Act
        var result = await customerRepo.GetCustomerList();

        // Assert
        Assert.NotNull(result);
    }
      [Fact]
    public async Task CreateCustomer_WithValidData()
    {
         // Arrange
        var fixture = new Fixture();
        var customerEntity = fixture.Create<CustomerEntity>();
        var customerDataModel = fixture.Create<CustomerDataModel>();

        var mapper = new Mock<IMapper>();
        mapper.Setup(m => m.Map<CustomerDataModel>(It.IsAny<CustomerEntity>()))
              .Returns(customerDataModel);

        var dapper = new Mock<IDapper>();
        dapper.Setup(d => d.Insert<CustomerDataModel>(It.IsAny<string>(), It.IsAny<CustomerDataModel>(),It.IsAny<CommandType>()))
              .Returns(customerDataModel); 

        var customerService = new CustomerRepository(dapper.Object,mapper.Object);

        // Act
        var result = await customerService.CreateCustomer(customerEntity);

        // Assert
        Assert.True(result);
    }
}
