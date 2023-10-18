using EShop.Application.IServices;
using EShop.Core.Interfaces;
using EShop.Application.Services;
using EShop.Application.Queries;
using EShop.Infrastracture.Persistence.DBHandler;
using EShop.Application.AutoMapper;
using MediatR;
using EShop.Infrastructure.Repositories;

namespace EShop.Extensions
{

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<ICartService, CartService>();
            serviceCollection.AddScoped<ICartRepository, CartRepository>();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
            serviceCollection.AddScoped<IShippingService, ShippingService>();
            serviceCollection.AddScoped<IShippingRepository, ShippingRepository>();
            serviceCollection.AddScoped<IDapper, DatabaseHandler>();
            serviceCollection.AddAutoMapper(typeof(ApplicationMapper));
            serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CustomerQuery).Assembly));
            //serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductListQueryHandler).Assembly));
            return serviceCollection;
        }
    }
}