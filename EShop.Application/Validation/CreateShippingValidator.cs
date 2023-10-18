using FluentValidation;
using EShop.Application.Dto;

namespace EShop.Application.Validation
{
    public class CreateShippingValidator : AbstractValidator<Shipping>
    {
        public CreateShippingValidator()
        {
            RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId must be greater than 0");

            RuleFor(item => item.Address1)
            .NotEmpty().WithMessage("AddressLine1 is required.")
            .MaximumLength(100).WithMessage("AddressLine1 cannot exceed 255 char");

            RuleFor(item => item.Address2)
            .NotEmpty().WithMessage("AddressLine1 is required.")
            .MaximumLength(100).WithMessage("AddressLine1 cannot exceed 255 char");

            RuleFor(item => item.IsActive)
               .NotNull().WithMessage("IsActive is required");
        }
    }
}