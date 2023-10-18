using AutoMapper;
using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Infrastructure.DataModel;

namespace EShop.Application.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Customer, CustomerEntity>().ReverseMap();
            CreateMap<CustomerEntity, CustomerDataModel>().ReverseMap();
            CreateMap<Product, ProductEntity>().ReverseMap();
            CreateMap<ProductDataModel, ProductEntity>().ReverseMap();

            CreateMap<Cart, CartEntity>().ReverseMap();
            CreateMap<CartItem, CartItemEntity>().ReverseMap();
            CreateMap<CartItemEntity, CartItemDataModel>().ReverseMap();
            CreateMap<CartEntity, CartDataModel>().ReverseMap();

            CreateMap<Order, OrderEntity>().ReverseMap();
            CreateMap<OrderEntity, OrderDataModel>().ReverseMap();

            CreateMap<Shipping, ShippingEntity>().ReverseMap();
            CreateMap<ShippingDataModel, ShippingEntity>().ReverseMap();
        }
    }
}