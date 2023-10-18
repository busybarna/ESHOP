using AutoMapper;
using MediatR;
using NSubstitute;
using Xunit;
using Moq;
using AutoFixture;
using EShop.Application.Commands;
using EShop.Application.Queries;
using EShop.Application.Services;
using EShop.Application.Dto;
using EShop.Core.Entities;

namespace EShop_Test;

public class CustomerServiceTest
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public CustomerServiceTest()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
    }
    [Fact]
    public async Task CreateCustomer_NotNullTest()
    {
        // Arrange
        var fixture = new Fixture();
        var customer = fixture.Create<Customer>();
        var customerEntity = fixture.Create<CustomerEntity>();

        var query = new CreateCustomerCommand { payLoad = customerEntity };
        _mediator.Send(query).Returns(true);
        _mapper.Map<bool>(_mediator.Send(query).Returns(true));
        var res = new CustomerService(_mediator, _mapper);

        // Act
        var resl = await res.CreateCustomer(customer);

        // Assert
        Assert.NotNull(resl);
    }

    [Fact]
    public async Task GetCustomerList_Empty()
    {
        // Arrange
        var customerList = new List<CustomerEntity> {
            new CustomerEntity {CustomerId = 1, CustomerNumber = "12345", Name = "Test"},
            new CustomerEntity {CustomerId = 2, CustomerNumber = "54321", Name = "Test new"},
        };

        var query = new CustomerQuery();
        _mediator.Send(query).Returns(customerList);
        _mapper.Map<List<Customer>>(_mediator.Send(query).Returns(customerList));
        var res = new CustomerService(_mediator, _mapper);

        // Act
        var resl = await res.GetCustomerList();

        // Assert
        Assert.Null(resl);
    }
}