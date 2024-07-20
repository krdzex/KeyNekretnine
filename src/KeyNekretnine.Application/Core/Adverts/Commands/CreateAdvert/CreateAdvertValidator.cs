using FluentValidation;

namespace KeyNekretnine.Application.Core.Adverts.Commands.CreateAdvert;
internal class CreateAdvertValidator :
    AbstractValidator<CreateAdvertCommand>
{
    public CreateAdvertValidator()
    {
        RuleFor(x => x.AdvertForCreating.Price)
            .NotEmpty()
                .WithMessage("Price is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Price cant be lower then 1");

        RuleFor(x => x.AdvertForCreating.DescriptionSr)
            .NotEmpty()
                .WithMessage("Description is required");

        RuleFor(x => x.AdvertForCreating.DescriptionSr)
            .NotEmpty()
                .WithMessage("Description is required");

        RuleFor(x => x.AdvertForCreating.HasWifi)
            .NotEmpty()
                .WithMessage("Required field");

        RuleFor(x => x.AdvertForCreating.HasGarage)
            .NotEmpty()
                .WithMessage("Required field");

        RuleFor(x => x.AdvertForCreating.IsUnderConstruction)
            .NotEmpty()
                .WithMessage("Required field");

        RuleFor(x => x.AdvertForCreating.Address)
            .NotEmpty()
                .WithMessage("Required field");

        RuleFor(x => x.AdvertForCreating.IsUrgent)
            .NotEmpty()
                .WithMessage("Required field");

        RuleFor(x => x.AdvertForCreating.HasTerrace)
            .NotEmpty()
                .WithMessage("Required field");

        RuleFor(x => x.AdvertForCreating.HasElevator)
            .NotEmpty()
                .WithMessage("Required field");

        RuleFor(x => x.AdvertForCreating.FloorSpace)
            .NotEmpty()
                .WithMessage("Floor space is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Floor space cant be lower then 1");

        RuleFor(x => x.AdvertForCreating.IsFurnished)
           .NotEmpty()
                .WithMessage("Required field");

        RuleFor(x => x.AdvertForCreating.Purpose)
            .NotEmpty()
                .WithMessage("Advert purpose is required")
            .Must(x => x > 0 && x < 4)
                .WithMessage("Invalid advert purpose");

        RuleFor(x => x.AdvertForCreating.NeighborhoodId)
            .NotEmpty()
                .WithMessage("Neighborhood is required")
            .Must(x => x > 0 && x < 1310)
                .WithMessage("Invalid neighborhood");

        RuleFor(x => x.AdvertForCreating.Type)
            .NotEmpty()
                .WithMessage("Advert type is required")
            .Must(x => x > 0 && x < 4)
                .WithMessage("Invalid advert type");

        RuleFor(x => x.AdvertForCreating.NoOfBathrooms)
            .NotEmpty()
                .WithMessage("Number of bathrooms is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Number of bathrooms cant be lower then 1");

        RuleFor(x => x.AdvertForCreating.NoOfBedrooms)
            .NotEmpty()
                .WithMessage("Number of badrooms is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Number of badrooms cant be lower then 1");

        RuleFor(x => x.AdvertForCreating.BuildingFloor)
            .NotEmpty()
                .WithMessage("Building floor is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Building floor cant be lower then 1");

        RuleFor(x => x.AdvertForCreating.CoverImageUrl)
            .NotEmpty()
            .WithMessage("Cover Image is required");

        RuleFor(u => u.AdvertForCreating.Longitude)
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

        RuleFor(u => u.AdvertForCreating.Latitude)
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

        RuleFor(x => x.AdvertForCreating.YearOfBuildingCreated)
            .NotEmpty()
            .Custom((yearOfBuildingCreated, context) =>
            {
                if (yearOfBuildingCreated != null)
                {
                    if (yearOfBuildingCreated > DateTime.Now.Year + 5 || yearOfBuildingCreated < 1980)
                    {
                        context.AddFailure("Invalid year");
                    }
                }
            });

        RuleFor(x => x.AdvertForCreating.Features)
            .Must(features => features == null || !features.Any(feature => string.IsNullOrWhiteSpace(feature)))
            .WithMessage("Features list cannot contain empty strings if it is not empty.");

        RuleFor(x => x.AdvertForCreating.ImageIds)
            .NotEmpty()
                .WithMessage("ImageIds list must contain at least one GUID.")
            .Must(imageIds => imageIds.Count > 0 && imageIds.Count < 12)
                .WithMessage("ImageIds list must contain between 1 and 11 GUIDs.");
    }
}