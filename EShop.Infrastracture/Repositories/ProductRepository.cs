using AutoMapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using System.Data;

namespace EShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDapper _dapper;
        private readonly IMapper _mapper;

        public ProductRepository(IDapper dapper, IMapper mapper)
        {
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<ProductEntity>> GetProductList()
        {
            List<ProductEntity> productList = new List<ProductEntity>();

            var productdatamodel = await Task.FromResult(_dapper.GetAll<ProductDataModel>(ProductDataModel.SelectQuery, null, commandType: CommandType.Text));
            productList = _mapper.Map<List<ProductEntity>>(productdatamodel);
            return productList;
        }

        public async Task<bool> CreatekProduct(ProductEntity productEntity)
        {
            var productModel = _mapper.Map<ProductDataModel>(productEntity);

            var result = await Task.FromResult(_dapper.Insert<ProductDataModel>(ProductDataModel.InsertQuery, productModel, commandType: CommandType.Text));
            return true;
        }
    }
}
