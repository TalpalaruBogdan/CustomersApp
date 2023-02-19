using CustomerService.DTOs;
using CustomerService.Models;
using FluentValidation;

namespace CustomerService.Validator
{
    public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
    {
        public UpdateCustomerDtoValidator()
        {
            RuleFor(x => x.Email).Matches(".*@.*..*");
        }
    }
}