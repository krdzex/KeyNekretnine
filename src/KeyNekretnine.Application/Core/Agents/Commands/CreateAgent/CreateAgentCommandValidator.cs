using FluentValidation;

namespace KeyNekretnine.Application.Core.Agents.Commands.CreateAgent;

internal class CreateAgentCommandValidator :
    AbstractValidator<CreateAgentCommand>
{
    public CreateAgentCommandValidator()
    {
        const double MaxImageSizeInBytes = 0.5 * 1024 * 1024;

        RuleFor(u => u.AgencyId)
            .NotEmpty().WithMessage("Agency id is required");

        RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("Max number of characters for first name is 50");

        RuleFor(u => u.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Max number of characters for first name is 50");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email adress is not valid")
            .MaximumLength(100).WithMessage("Max number of characters for email is 100");

        RuleFor(u => u.Description)
            .MaximumLength(1000).WithMessage("Max number of characters for description is 1000");

        RuleFor(u => u.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MaximumLength(50).WithMessage("Max number of characters for phone number is 50");

        RuleFor(u => u.Facebook)
            .Must(BeAValidUrl).WithMessage("Invalid URL for facebook")
            .MaximumLength(300).WithMessage("Max number of characters for facebook url is 300");

        RuleFor(u => u.Twitter)
            .Must(BeAValidUrl).WithMessage("Invalid URL for twitter")
            .MaximumLength(300).WithMessage("Max number of characters for twitter url is 300");

        RuleFor(u => u.Instagram)
            .Must(BeAValidUrl).WithMessage("Invalid URL for instagram")
            .MaximumLength(300).WithMessage("Max number of characters for instagram url is 300");

        RuleFor(u => u.Linkedin)
            .Must(BeAValidUrl).WithMessage("Invalid URL for linkedin")
            .MaximumLength(300).WithMessage("Max number of characters for linkedin url is 300");


        RuleFor(u => u.Image)
            .Must(image => image.Length <= MaxImageSizeInBytes).WithMessage("The image must not exceed 0.5MB in size.")
            .When(u => u.Image is not null);

        RuleFor(u => u.LanguageIds)
            .Must(languagesIds => !ContainsDuplicates(languagesIds)).WithMessage("Duplicate language is not allowed")
            .ForEach(id => id
                .GreaterThanOrEqualTo(1).WithMessage("Invalid language")
                .LessThanOrEqualTo(52).WithMessage("Invalid language"))
            .When(languagesIds => languagesIds is not null);
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

        if (ids is not null)
        {
            foreach (var id in ids)
            {
                if (!set.Add(id))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
