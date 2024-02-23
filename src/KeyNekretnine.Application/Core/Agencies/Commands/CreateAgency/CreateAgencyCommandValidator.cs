using FluentValidation;

namespace KeyNekretnine.Application.Core.Agencies.Commands.CreateAgency;
internal class CreateAgencyCommandValidator :
    AbstractValidator<CreateAgencyCommand>
{
    public CreateAgencyCommandValidator()
    {

        RuleFor(c => c.AgencyName)
            .NotEmpty().WithMessage("Agency Name is required")
            .MaximumLength(50).WithMessage("Maximum length of agency name is 50");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required field")
            .EmailAddress().WithMessage("Email adress is not valid");

        RuleFor(c => c.Password)
            .Custom((password, context) =>
            {
                var errors = new List<string>();
                if (!password.Any(char.IsUpper))
                    errors.Add("Password must contain at least one uppercase letter.");
                if (!password.Any(char.IsDigit))
                    errors.Add("Password must contain at least one number.");
                if (!password.Any(c => !char.IsLetterOrDigit(c)))
                    errors.Add("Password must contain at least one special character.");
                if (errors.Any())
                    context.AddFailure(string.Join(" ", errors));
            });
    }
}