using FluentValidation;
using InlämningSalonn.DTOs;
namespace InlämningSalonn.Validators;

public class CustomerDtoValidator : AbstractValidator<SimpleCustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Namn krävs.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-postadress krävs.")
            .EmailAddress().WithMessage("Ogiltig e-postadress.");
    }
}
