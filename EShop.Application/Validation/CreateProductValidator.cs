using FluentValidation;
using EShop.Application.Dto;

namespace EShop.Application.Validation
{
    public class CreateProductValidator : AbstractValidator<Product>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product Name is required.");

            RuleFor(item => item.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}