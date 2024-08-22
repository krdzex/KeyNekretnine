using FluentValidation;

namespace KeyNekretnine.Application.Core.Agents.Commands.UpdateAgent;

internal class UpdateAgentCommandValidator :
    AbstractValidator<UpdateAgentCommand>
{
    public UpdateAgentCommandValidator()
    {
        const double MaxImageSizeInBytes = 0.5 * 1024 * 1024;

        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty.")
            .MaximumLength(50).WithMessage("Max number of characters for last name is 50")
            .When(c => c.LastName is not null);

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Last name cannot be empty.")
            .MaximumLength(50).WithMessage("Max number of characters for last name is 50")
            .When(c => c.LastName is not null);

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email adress is not valid")
            .MaximumLength(100).WithMessage("Max number of characters for email is 100")
            .When(c => c.Email is not null);

        RuleFor(u => u.Description)
            .MaximumLength(1000).WithMessage("Max number of characters for description is 1000")
            .When(c => c.Description is not null);

        RuleFor(u => u.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MaximumLength(50).WithMessage("Max number of characters for phone number is 50")
            .When(c => c.PhoneNumber is not null);

        RuleFor(c => c.Facebook)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().When(c => !string.IsNullOrEmpty(c.Facebook))
            .Must(BeAValidUrl).WithMessage("Invalid URL for Facebook")
            .MaximumLength(300).WithMessage("Max number of characters for Facebook URL is 300")
            .When(c => !string.IsNullOrEmpty(c.Facebook));

        RuleFor(c => c.Twitter)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().When(c => !string.IsNullOrEmpty(c.Twitter))
            .Must(BeAValidUrl).WithMessage("Invalid URL for Twitter")
            .MaximumLength(300).WithMessage("Max number of characters for Twitter URL is 300")
            .When(c => !string.IsNullOrEmpty(c.Twitter));

        RuleFor(c => c.Instagram)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().When(c => !string.IsNullOrEmpty(c.Instagram))
            .Must(BeAValidUrl).WithMessage("Invalid URL for Instagram")
            .MaximumLength(300).WithMessage("Max number of characters for Instagram URL is 300")
            .When(c => !string.IsNullOrEmpty(c.Instagram));

        RuleFor(c => c.Linkedin)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().When(c => !string.IsNullOrEmpty(c.Linkedin))
            .Must(BeAValidUrl).WithMessage("Invalid URL for Linkedin")
            .MaximumLength(300).WithMessage("Max number of characters for Linkedin URL is 300")
            .When(c => !string.IsNullOrEmpty(c.Linkedin));

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
