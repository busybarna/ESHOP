using AutoMapper;
using Dapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using System.Data;

namespace EShop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDapper _dapper;
        private readonly IMapper _mapper;

        public OrderRepository(IDapper dapper, IMapper mapper)
        {
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<OrderEntity>> GetCustomerOrder(int CustomerId)
        {
            List<OrderEntity> orderList = new List<OrderEntity>();

            OrderDataModel orderDataModel = new OrderDataModel();
            orderDataModel.CustomerId = CustomerId;

            var orderDataModels = await Task.FromResult(_dapper.GetAll<OrderDataModel>(OrderDataModel.SelectQuery, orderDataModel, commandType: CommandType.Text));
            orderList = _mapper.Map<List<OrderEntity>>(orderDataModels);
            return orderList;
        }

        public async Task<bool> CreateOrder(int CustomerId)
        {
            Random generator = new Random();
            string rand = generator.Next(0, 1000000).ToString("D6");

            OrderDataModel customerDataModel = new OrderDataModel();
            customerDataModel.CustomerId = CustomerId;
            customerDataModel.OrderNumber = rand;

            var result = await Task.FromResult(_dapper.Insert<OrderDataModel>(OrderDataModel.InsertQuery, customerDataModel, commandType: CommandType.Text));
            return true;
        }
    }
}
