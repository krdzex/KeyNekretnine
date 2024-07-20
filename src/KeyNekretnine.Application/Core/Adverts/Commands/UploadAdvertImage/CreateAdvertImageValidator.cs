using FluentValidation;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UploadAdvertImage;
internal class CreateAdvertImageValidator :
    AbstractValidator<CreateAdvertImageCommand>
{
    public CreateAdvertImageValidator()
    {
        const double MaxImageSizeInBytes = 0.5 * 1024 * 1024;

        RuleFor(x => x.Image)
            .NotEmpty()
                .WithMessage("Image cannot be empty.")
            .Must(image => image.Length <= MaxImageSizeInBytes)
                .WithMessage("Image size must not exceed 0.5 MB.");
    }
}