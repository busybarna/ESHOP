using AutoMapper;
using Dapper;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using EShop.Infrastructure.DataModel;
using System.Data;

namespace EShop.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDapper _dapper;
        private readonly IMapper _mapper;

        public CartRepository(IDapper dapper, IMapper mapper)
        {
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<CartItemEntity>> GetCart(int CustomerId)
        {
            List<CartItemEntity> cartItemList = new List<CartItemEntity>();

            CartItemDataModel cartItemDataModel = new CartItemDataModel();
            cartItemDataModel.CustomerId = CustomerId;

            var cartitem = await Task.FromResult(_dapper.GetAll<CartItemDataModel>(CartItemDataModel.SelectQuery, cartItemDataModel, commandType: CommandType.Text));
            cartItemList = _mapper.Map<List<CartItemEntity>>(cartitem);
            return cartItemList;
        }

        public async Task<bool> CreateCart(CartEntity cartEntity)
        {
            CartDataModel cartDataModel = new CartDataModel();

            cartDataModel.CustomerId = cartEntity.CustomerId;

            var item = new List<CartItemDataModel>();
            foreach (var product in cartEntity.Items)
            {
                item.Add(new CartItemDataModel { CustomerId = cartEntity.CustomerId, ProductId = product.ProductId, Quantity = product.Quantity });
            }
            cartDataModel.Items = item;

            await Task.FromResult(_dapper.Execute<CartItemDataModel>(CartItemDataModel.InsertQuery, cartDataModel.Items, commandType: CommandType.Text));

            return true;
        }
    }
}
