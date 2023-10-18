using FluentValidation;
using EShop.Application.Dto;

namespace OnlineLibraryShop.Application.Validation
{
    public class CartValidator : AbstractValidator<Cart>
    {
        public CartValidator()
        {
            RuleFor(order => order.CustomerId)
            .NotEmpty().WithMessage("Customer Id is required.");

            RuleForEach(order => order.ProductItems)
           .SetValidator(new PurchaseItemValidator());
        }

        public class PurchaseItemValidator : AbstractValidator<CartItem>
        {
            public PurchaseItemValidator()
            {
                RuleFor(item => item.ProductId)
                    .NotEmpty().WithMessage("Product Id is required.");

                RuleFor(item => item.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            }
        }
    }
}