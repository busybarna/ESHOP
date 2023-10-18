using AutoMapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using System.Data;

namespace EShop.Infrastructure.Repositories
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly IDapper _dapper;
        private readonly IMapper _mapper;

        public ShippingRepository(IDapper dapper, IMapper mapper)
        {
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<ShippingEntity>> GetShippingList(int CustomerId)
        {
            List<ShippingEntity> shippingEntities = new List<ShippingEntity>();

            ShippingDataModel shippingDataModel = new ShippingDataModel();
            shippingDataModel.CustomerId = CustomerId;

            var shippingData = await Task.FromResult(_dapper.GetAll<ShippingDataModel>(ShippingDataModel.SelectQuery, shippingDataModel, commandType: CommandType.Text));
            shippingEntities = _mapper.Map<List<ShippingEntity>>(shippingData);
            return shippingEntities;
        }

        public async Task<bool> CreateShipping(ShippingEntity shippingEntity)
        {
            var shippingDataModel = _mapper.Map<ShippingDataModel>(shippingEntity);

            var result = await Task.FromResult(_dapper.Insert<ShippingDataModel>(ShippingDataModel.InsertQuery, shippingDataModel, commandType: CommandType.Text));
            return true;
        }
    }
}
