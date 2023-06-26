using Application.Core.Agencies.Commands.UpdateAgency;
using FluentValidation;
using System.Globalization;

namespace Application.Validators;
public sealed class UpdateAgencyCommandValidator :
    AbstractValidator<UpdateAgencyCommand>
{
    public UpdateAgencyCommandValidator()
    {
        const double MaxImageSizeInBytes = 0.5 * 1024 * 1024;

        RuleFor(u => u.UpdateAgency.Name)
            .NotEmpty()
                .WithMessage("Field Name cant be empty")
            .MaximumLength(50)
                .WithMessage("Max number of characters is 50"); ;

        RuleFor(u => u.UpdateAgency.Description)
            .MaximumLength(1000)
            .WithMessage("Max number of characters is 1000");

        RuleFor(u => u.UpdateAgency.Email)
            .EmailAddress()
                .WithMessage("Email adress is not valid")
            .MaximumLength(100)
                .WithMessage("Max number of characters is 100");

        RuleFor(u => u.UpdateAgency.Address)
            .MaximumLength(200)
            .WithMessage("Max number of characters is 200");

        RuleFor(u => u.UpdateAgency.FacebookUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.UpdateAgency.TwitterUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.UpdateAgency.FacebookUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.UpdateAgency.InstagramUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.UpdateAgency.LinkedinUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.UpdateAgency.Longitude)
            .Custom((longitude, context) =>
            {
                if (longitude is not null)
                {
                    if (longitude <= -180 || longitude >= 180)
                    {
                        context.AddFailure("Longitude need to be between -180 and 180");
                    }
                }
            });

        RuleFor(u => u.UpdateAgency.Latitude)
            .Custom((latitude, context) =>
            {
                if (latitude is not null)
                {
                    if (latitude <= -90 || latitude >= 90)
                    {
                        context.AddFailure("Latitude need to be between -90 and 90");
                    }
                }
            });

        RuleFor(u => u.UpdateAgency.WebsiteUrl)
            .Must(BeAValidUrl)
                .WithMessage("Invalid URL")
            .MaximumLength(200)
                .WithMessage("Max number of characters is 200");

        RuleFor(u => u.UpdateAgency.WorkStartTime)
            .Must(BeAValidTime)
            .WithMessage("Invalid time");

        RuleFor(u => u.UpdateAgency.WorkEndTime)
            .Must(BeAValidTime)
            .WithMessage("Invalid time");


        RuleFor(u => u.UpdateAgency.Image)
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

        RuleFor(u => u.UpdateAgency.LanguageId)
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

    private static bool BeAValidTime(string arg)
    {
        if (arg is not null)
        {
            TimeSpan time;

            return TimeSpan.TryParseExact(arg, @"hh\:mm", CultureInfo.CurrentCulture, out time);
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
