using FluentValidation;

namespace KeyNekretnine.Application.Core.Agencies.Commands.UpdateAgency;
internal class UpdateAgencyCommandValidator :
    AbstractValidator<UpdateAgencyCommand>
{
    public UpdateAgencyCommandValidator()
    {
        const double MaxImageSizeInBytes = 0.5 * 1024 * 1024;

        RuleFor(c => c.Name)
            .MaximumLength(50).WithMessage("Max number of characters is 50")
            .When(c => c.Name is not null);


        RuleFor(c => c.Description)
            .MaximumLength(1000)
            .WithMessage("Max number of characters is 1000")
            .When(c => c.Description is not null);


        RuleFor(c => c.Email)
            .EmailAddress().WithMessage("Email adress is not valid")
            .MaximumLength(100).WithMessage("Max number of characters is 100")
            .When(c => c.Email is not null);

        RuleFor(c => c.Address)
            .MaximumLength(200).WithMessage("Max number of characters is 200")
            .When(c => c.Address is not null);

        RuleFor(c => c.Facebook)
            .Must(BeAValidUrl).WithMessage("Invalid URL for facebook")
            .MaximumLength(300).WithMessage("Max number of characters for facebook url is 300")
            .When(c => c.Facebook is not null);

        RuleFor(c => c.Twitter)
            .Must(BeAValidUrl).WithMessage("Invalid URL for twitter")
            .MaximumLength(300).WithMessage("Max number of characters for twitter url is 300")
            .When(c => c.Twitter is not null);

        RuleFor(c => c.Instagram)
            .Must(BeAValidUrl).WithMessage("Invalid URL for instagram")
            .MaximumLength(300).WithMessage("Max number of characters for instagram url is 300")
            .When(c => c.Instagram is not null);

        RuleFor(c => c.Linkedin)
            .Must(BeAValidUrl).WithMessage("Invalid URL for linkedin")
            .MaximumLength(300).WithMessage("Max number of characters for linkedin url is 300")
            .When(c => c.Linkedin is not null);

        RuleFor(c => c.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude need to be between -180 and 180")
            .When(c => c.Longitude is not null);

        RuleFor(c => c.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude need to be between -90 and 90")
            .When(c => c.Latitude is not null);

        RuleFor(c => c.WebsiteUrl)
            .Must(BeAValidUrl).WithMessage("Invalid URL")
            .MaximumLength(200).WithMessage("Max number of characters is 200");

        RuleFor(c => c.Image)
            .Must(image => image.Length <= MaxImageSizeInBytes).WithMessage("The image must not exceed 0.5MB in size.")
            .When(u => u.Image is not null);

        RuleFor(c => c.LanguageIds)
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

    //private static bool BeAValidTime(DateTime? arg)
    //{
    //    if (arg is not null)
    //    {
    //        TimeSpan time;

    //        return TimeSpan.TryParseExact(arg, @"hh\:mm", CultureInfo.CurrentCulture, out time);
    //    }

    //    return true;
    //}

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
