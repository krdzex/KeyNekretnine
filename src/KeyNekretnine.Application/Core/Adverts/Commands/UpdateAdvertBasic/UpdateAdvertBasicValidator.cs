using FluentValidation;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertBasic;
internal class UpdateAdvertBasicValidator :
    AbstractValidator<UpdateAdvertBasicCommand>
{
    public UpdateAdvertBasicValidator()
    {
        RuleFor(x => x.BasicUpdateData.Price)
            .NotEmpty()
                .WithMessage("Price is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Price cant be lower then 1");

        RuleFor(x => x.BasicUpdateData.DescriptionSr)
            .NotEmpty()
                .WithMessage("Description is required");

        RuleFor(x => x.BasicUpdateData.DescriptionSr)
            .NotEmpty()
                .WithMessage("Description is required");

        RuleFor(x => x.BasicUpdateData.HasWifi)
            .Must(value => value == true || value == false)
                .WithMessage("Required field");

        RuleFor(x => x.BasicUpdateData.HasGarage)
            .Must(value => value == true || value == false)
                .WithMessage("Required field");

        RuleFor(x => x.BasicUpdateData.IsUnderConstruction)
            .Must(value => value == true || value == false)
                .WithMessage("Required field");

        RuleFor(x => x.BasicUpdateData.IsUrgent)
            .Must(value => value == true || value == false)
                .WithMessage("Required field");

        RuleFor(x => x.BasicUpdateData.HasTerrace)
            .Must(value => value == true || value == false)
                .WithMessage("Required field");

        RuleFor(x => x.BasicUpdateData.HasElevator)
            .Must(value => value == true || value == false)
                .WithMessage("Required field");

        RuleFor(x => x.BasicUpdateData.FloorSpace)
            .NotEmpty()
                .WithMessage("Floor space is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Floor space cant be lower then 1");

        RuleFor(x => x.BasicUpdateData.IsFurnished)
            .Must(value => value == true || value == false)
                .WithMessage("Required field");

        RuleFor(x => x.BasicUpdateData.Purpose)
            .NotEmpty()
                .WithMessage("Advert purpose is required")
            .Must(x => x > 0 && x < 4)
                .WithMessage("Invalid advert purpose");


        RuleFor(x => x.BasicUpdateData.Type)
            .NotEmpty()
                .WithMessage("Advert type is required")
            .Must(x => x > 0 && x < 4)
                .WithMessage("Invalid advert type");

        RuleFor(x => x.BasicUpdateData.NoOfBathrooms)
            .NotEmpty()
                .WithMessage("Number of bathrooms is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Number of bathrooms cant be lower then 1");

        RuleFor(x => x.BasicUpdateData.NoOfBedrooms)
            .NotEmpty()
                .WithMessage("Number of badrooms is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Number of badrooms cant be lower then 1");

        RuleFor(x => x.BasicUpdateData.BuildingFloor)
            .NotEmpty()
                .WithMessage("Building floor is required")
            .GreaterThanOrEqualTo(1)
                .WithMessage("Building floor cant be lower then 1");


        RuleFor(x => x.BasicUpdateData.YearOfBuildingCreated)
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