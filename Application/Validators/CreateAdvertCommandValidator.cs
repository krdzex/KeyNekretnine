using Application.Core.Adverts.Commands.CreateAdvert;
using FluentValidation;

namespace Application.Validators
{
    public sealed class CreateAdvertCommandValidator : AbstractValidator<CreateAdvertCommand>
    {
        public CreateAdvertCommandValidator()
        {

            RuleFor(x => x.AdvertForCreating.Price)
                .NotEmpty()
                .WithMessage("Price is required")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Price cant be lower then 1");

            RuleFor(x => x.AdvertForCreating.HasWifi)
                .NotEmpty()
                .WithMessage("Required field");

            RuleFor(x => x.AdvertForCreating.HasGarage)
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

            RuleFor(x => x.AdvertForCreating.AdvertPurposeId)
                .NotEmpty()
                .WithMessage("Advert purpose is required")
                .Must(x => x > 0 && x < 4)
                .WithMessage("Invalid advert purpose");

            RuleFor(x => x.AdvertForCreating.AdvertTypeId)
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

            RuleFor(x => x.AdvertForCreating.CoverImage)
                .NotEmpty()
                .WithMessage("Cover Image is required");

            RuleFor(x => x.AdvertForCreating.ImageFiles)
                .Custom((images, context) =>
                {
                    if (images != null)
                    {
                        if (images.Count > 12)
                        {
                            context.AddFailure("No more then 12 images");
                        }
                    }
                    else
                    {
                        context.AddFailure("At least one image is required");
                    }
                });

            RuleFor(x => x.AdvertForCreating.Longitude)
               .NotEmpty()
               .WithMessage("Required field");

            RuleFor(x => x.AdvertForCreating.Latitude)
               .NotEmpty()
               .WithMessage("Required field");

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
        }
    }
}
