using MediatR;
using AutoMapper;
using EShop.Core.Models;
using EShop.Application.Commands;
using EShop.Application.Queries;
using EShop.Core.Constansts;
using EShop.Application.IServices;
using EShop.Application.Dto;

namespace EShop.Application.Services
{

    public class OrderService : IOrderService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<bool> CreateOrder(int CustomerId)
        {
            var result = false;

            var isCustomerExists = await _mediator.Send(new GetCustomerByIdQuery(CustomerId));
            var cartItem = await _mediator.Send(new CartQuery(CustomerId));

            if (isCustomerExists && cartItem.Count > 0)
            {
                var query = new CreateOrderCommand(CustomerId);
                result = await _mediator.Send(query);
            }
            return result;
        }
        public async Task<CustomerOrder> GetCustomerOrder(int CustomerId)
        {
            var query = new CustomerOrderQuery(CustomerId);
            var result = await _mediator.Send(query);

            CustomerOrder customerOrderResponse = new CustomerOrder();

            var item = new List<string>();
            var shippingDetails = new List<string>();

            double Total = 0;

            if (result.Count > 0)
            {
                string customerNumber = result.Where(x => x.CustomerId == CustomerId).FirstOrDefault().CustomerNumber;
                string OrderNumber = result.Where(x => x.CustomerId == CustomerId).FirstOrDefault().OrderNumber;

                bool IsLoyaltyMembership = result.Where(x => x.CustomerId == CustomerId).FirstOrDefault().IsLoyaltyMembership;
                string address1 = result.Where(x => x.CustomerId == CustomerId).FirstOrDefault().address1;
                string address2 = result.Where(x => x.CustomerId == CustomerId).FirstOrDefault().address2;

                foreach (var product in result)
                {
                    Total = Total + product.NetAmount;
                    string productName = $"{product.ProductName} -Qty: {product.Quantity}";
                    if (item.Any(s => s.Contains(product.ProductName)))
                    {
                        int index = item.FindIndex(s => s.Contains(product.ProductName));
                        item.RemoveAt(index);
                        decimal totalPurchases = result.Where(item => item.ProductName == product.ProductName)
                                              .Sum(item => item.Quantity);
                        item.Add($"{product.ProductName} -Qty: {totalPurchases}");
                    }
                    else
                    {
                        item.Add(productName);
                        int index = item.FindIndex(s => s.Contains(product.ProductName));
                    }
                }

                double discountTotal = 0;

                if (IsLoyaltyMembership)
                {
                    discountTotal = Total * Constansts.discountPercentage;
                    Total = Total - discountTotal + Constansts.MembershipAmount;
                    item.Add("EasyGroceries loyalty membership");
                }
                if (!string.IsNullOrEmpty(address1) || !string.IsNullOrEmpty(address2))
                {
                    shippingDetails.Add(address1);
                    shippingDetails.Add(address2);
                }

                customerOrderResponse.CustomerNumber = customerNumber;
                customerOrderResponse.OrderNumber = OrderNumber;
                customerOrderResponse.Total = Total;
                customerOrderResponse.itemlist = item;
                customerOrderResponse.Shipping = shippingDetails;
            }
            return _mapper.Map<CustomerOrder>(customerOrderResponse);
        }
    }

}