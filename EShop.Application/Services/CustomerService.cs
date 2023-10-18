using MediatR;
using AutoMapper;
using EShop.Application.Commands;
using EShop.Application.Queries;
using EShop.Application.IServices;
using EShop.Application.Dto;
using EShop.Core.Entities;

namespace EShop.Application.Services{

    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CustomerService(IMediator mediator,IMapper mapper){
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<List<Customer>> GetCustomerList()
        {
            List<Customer> customers = new List<Customer>();
            var result = await _mediator.Send(new CustomerQuery());
            customers = _mapper.Map<List<Customer>>(result);
            return customers;
        }
        public async Task<bool> CreateCustomer(Customer customer)
        {
            CustomerEntity customerEntity = new CustomerEntity();
            customerEntity = _mapper.Map<CustomerEntity>(customer);
            var query = new CreateCustomerCommand() {payLoad = customerEntity};
            var result = await _mediator.Send(query);
            return  result;
        }
      
    }
}