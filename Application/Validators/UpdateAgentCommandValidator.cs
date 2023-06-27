using Application.Core.Agents.Commands.UpdateAgent;
using FluentValidation;

namespace Application.Validators;
public sealed class UpdateAgentCommandValidator :
    AbstractValidator<UpdateAgentCommand>
{
    public UpdateAgentCommandValidator()
    {
        const double MaxImageSizeInBytes = 0.5 * 1024 * 1024;


        RuleFor(u => u.Agent.FirstName)
            .NotEmpty()
                .WithMessage("Field is required")
            .MaximumLength(30)
                .WithMessage("Max number of characters is 30");

        RuleFor(u => u.Agent.LastName)
             .NotEmpty()
                .WithMessage("Field is required")
            .MaximumLength(30)
                .WithMessage("Max number of characters is 30");

        RuleFor(u => u.Agent.Email)
            .NotEmpty()
                .WithMessage("Field is required")
            .EmailAddress()
                .WithMessage("Email adress is not valid")
            .MaximumLength(100)
                .WithMessage("Max number of characters is 100");

        RuleFor(u => u.Agent.Description)
            .MaximumLength(1000)
                .WithMessage("Max number of characters is 1000");

        RuleFor(u => u.Agent.PhoneNumber)
            .NotEmpty()
                .WithMessage("Field is required");

        RuleFor(u => u.Agent.FacebookUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.Agent.TwitterUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.Agent.FacebookUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.Agent.InstagramUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.Agent.LinkedinUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");


        RuleFor(u => u.Agent.Image)
            .Custom((image, context) =>
            {
                if (image != null)
                {
                    if (image.Length > MaxImageSizeInBytes)
                    {
                        context.AddFailure("The image must not exceed 0.5MB in size.");
                    }
                }
            });

        RuleFor(u => u.Agent.LanguageId)
            .Custom((languagesIds, context) =>
            {
                if (languagesIds is not null)
                {
                    if (ContainsDuplicates(languagesIds))
                    {
                        context.AddFailure("Duplicate language is not allowed");
                    }
                    else
                    {
                        foreach (var id in languagesIds)
                        {
                            if (id < 1 || id > 52)
                            {
                                context.AddFailure("Invalid language");
                            }
                        }
                    }
                }
            });
    }

    private static bool BeAValidUrl(string arg)
    {
        if (arg is not null)
        {
            Uri result;

            return Uri.TryCreate(arg, UriKind.Absolute, out result);
        }

        return true;
    }

    public bool ContainsDuplicates<T>(IEnumerable<T> ids)
    {
        HashSet<T> set = new();

        foreach (var id in ids)
        {
            if (!set.Add(id))
            {
                return true;
            }
        }

        return false;
    }
}
