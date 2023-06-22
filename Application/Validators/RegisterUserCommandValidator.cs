using Application.Core.Auth.Commands.UserRegistration;
using FluentValidation;

namespace Application.Validators;
public sealed class RegisterUserCommandValidator :
    AbstractValidator<UserRegistrationCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.RegistrationUser.FirstName)
            .NotEmpty()
            .WithMessage("Field First Name cant be empty");
        RuleFor(u => u.RegistrationUser.LastName)
            .NotEmpty()
            .WithMessage("Field Last Name cant be empty");
        RuleFor(u => u.RegistrationUser.Email)
            .NotEmpty()
                .WithMessage("Email is required field")
            .EmailAddress()
                .WithMessage("Email adress is not valid");

        RuleFor(x => x.RegistrationUser.Password)
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
