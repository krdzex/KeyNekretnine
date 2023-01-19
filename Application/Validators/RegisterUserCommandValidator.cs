using Application.Commands.UserCommands;
using FluentValidation;

namespace Application.Validators
{
    public sealed class RegisterUserCommandValidator :
    AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.RegistrationUser.FirstName).NotEmpty();
            RuleFor(u => u.RegistrationUser.LastName).NotEmpty();
            RuleFor(u => u.RegistrationUser.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.RegistrationUser.Password)
                .MinimumLength(6)
                .Must(x => x.Any(char.IsUpper))
                    .WithMessage("Password must contain at least one uppercase letter.")
                .Must(x => x.Any(char.IsDigit))
                    .WithMessage("Password must contain at least one number.")
                .Must(x => x.Any(c => !char.IsLetterOrDigit(c)))
                    .WithMessage("Password must contain at least one special character.");
        }
    }
}
