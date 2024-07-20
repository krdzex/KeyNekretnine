using FluentValidation;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
internal class UpdateAdvertLocationValidator :
    AbstractValidator<UpdateAdvertLocationCommand>
{
    public UpdateAdvertLocationValidator()
    {
        RuleFor(x => x.LocationUpdateRequest.Address)
            .NotEmpty()
                .WithMessage("Required field");

        RuleFor(x => x.LocationUpdateRequest.NeighborhoodId)
            .NotEmpty()
                .WithMessage("Neighborhood is required")
            .Must(x => x > 0 && x < 1310)
                .WithMessage("Invalid neighborhood");

        RuleFor(x => x.LocationUpdateRequest.Longitude)
            .Custom((longitude, context) =>
            {
                if (longitude is not null)
                {
                    if (longitude <= -180 || longitude >= 180)
                    {
                        context.AddFailure("Longitude need to be between -180 and 180");
                    }
                }
                else
                {
                    context.AddFailure("Longitude is required");
                }
            });

        RuleFor(x => x.LocationUpdateRequest.Latitude)
            .Custom((latitude, context) =>
            {
                if (latitude is not null)
                {
                    if (latitude <= -90 || latitude >= 90)
                    {
                        context.AddFailure("Latitude need to be between -90 and 90");
                    }
                }
                else
                {
                    context.AddFailure("Latitude is required");
                }
            });
    }
}