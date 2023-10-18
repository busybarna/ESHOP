using AutoMapper;
using Dapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Core.Models;
using EShop.Infrastructure.DataModel;
using System.Data;

namespace EShop.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDapper _dapper;
        private readonly IMapper _mapper;

        public CustomerRepository(IDapper dapper, IMapper mapper)
        {
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<CustomerEntity>> GetCustomerList()
        {
            List<CustomerEntity> customerList = new List<CustomerEntity>();

            var customerdatamodel = await Task.FromResult(_dapper.GetAll<CustomerDataModel>(CustomerDataModel.SelectQuery, null, commandType: CommandType.Text));
            customerList = _mapper.Map<List<CustomerEntity>>(customerdatamodel);
            return customerList;
        }

        public async Task<bool> CreateCustomer(CustomerEntity customerEntity)
        {
            var CustomerModel = _mapper.Map<CustomerDataModel>(customerEntity);

            var result = await Task.FromResult(_dapper.Insert<CustomerDataModel>(CustomerDataModel.InsertQuery, CustomerModel, commandType: CommandType.Text));
            return true;
        }

        public async Task<bool> GetCustomerById(int CustomerId)
        {
            var isCustomerExists = false;
            CustomerDataModel customerDataModel = new CustomerDataModel();
            customerDataModel.CustomerId = CustomerId;

            var result = await Task.FromResult(_dapper.GetAll<CustomerDataModel>(CustomerDataModel.SelectCustomerById, customerDataModel, commandType: CommandType.Text));

            if (result.Count > 0)
                isCustomerExists = true;

            return isCustomerExists;
        }
    }
}
