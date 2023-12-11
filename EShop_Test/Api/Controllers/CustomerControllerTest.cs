using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using AutoFixture;
using EShop.Application.IServices;
using EShop.Controllers;
using EShop.Application.Dto;

namespace EShop_Test.Api.Controllers;

public class CustomerControllerTest
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomerController> _logger;
    public CustomerControllerTest()
    {
        _customerService = Substitute.For<ICustomerService>();
        _logger = Substitute.For<ILogger<CustomerController>>();
    }

    [Fact]
    public async Task GetCustomerList_ShouldReturnValue()
    {
        var customers = new List<Customer>{
            new Customer {CustomerId = 1, CustomerNumber = "12345",Name = "Test Customer"},
            new Customer {CustomerId = 2, CustomerNumber = "54321",Name = "Test Customer new"}
        };

        _customerService.GetCustomerList().Returns(customers);
        var mainresult = new CustomerController(_customerService, _logger);
        var result = await mainresult.GetCustomerList();
        Assert.NotNull(result);
    }
    [Fact]
    public async Task CreateCustomerController_ShouldReturnValue()
    {
        var fixture = new Fixture();
        var customer = fixture.Create<Customer>();

        _customerService.CreateCustomer(customer).Returns(true);
        var mainresult = new CustomerController(_customerService, _logger);
        var result = await mainresult.CreateCustomer(customer);
        Assert.NotNull(result);
    }
}
