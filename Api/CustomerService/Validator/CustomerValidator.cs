using CustomerService.Models;
using FluentValidation;

namespace CustomerService.Validator
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Email).Matches(".*@.*..*");
        }
    }
}
