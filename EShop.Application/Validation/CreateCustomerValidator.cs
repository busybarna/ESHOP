using FluentValidation;
using EShop.Application.Dto;

namespace EShop.Application.Validation
{
    public class CreateCustomerValidator : AbstractValidator<Customer>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer Name is required.");

            RuleFor(x => x.CustomerNumber)
            .NotEmpty().WithMessage("Customer Number is required.");

            RuleFor(item => item.IsLoyaltyMembership)
            .Must(ValidTypeMembershipType)
            .WithMessage("Membership Type must be 'LoyaltyMembership' or 'no  membership'");
        }
        private bool ValidTypeMembershipType(bool membershiptype)
        {
            return membershiptype == true || membershiptype == false;
        }
    }
}