using FluentValidation;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserRegistration;
internal class UserRegistrationCommandValidator :
    AbstractValidator<UserRegistrationCommand>
{
    public UserRegistrationCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
                .WithMessage("Email is required field")
            .EmailAddress()
                .WithMessage("Email adress is not valid");

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
