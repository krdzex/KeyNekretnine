using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Shared;
using KeyNekretnine.Domain.ValueObjects;

namespace KeyNekretnine.Application.Core.Agencies.Commands.UpdateAgency;
internal sealed class UpdateAgencyHandler : ICommandHandler<UpdateAgencyCommand>
{
    private readonly IAgencyRepository _agencyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;
    private readonly IImageToDeleteRepository _imageToDeleteRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    public UpdateAgencyHandler(
        IAgencyRepository agencyRepository,
        IUnitOfWork unitOfWork,
        IImageService imageService,
        IImageToDeleteRepository imageToDeleteRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _agencyRepository = agencyRepository;
        _unitOfWork = unitOfWork;
        _imageService = imageService;
        _imageToDeleteRepository = imageToDeleteRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(UpdateAgencyCommand request, CancellationToken cancellationToken)
    {
        var agency = await _agencyRepository.GetByIdWithLanguagesAsync(request.AgencyId, cancellationToken);

        if (agency is null)
        {
            return Result.Failure(AgencyErrors.NotFound);
        }

        if (agency.UserId != request.UserId)
        {
            return Result.Failure(AgencyErrors.NotOwner);

        }

        agency.Update(
            AgencyName.Create(request.Name ?? agency.Name.Value),
            Location.Create(
                request.Address ?? agency.Location.Address,
                request.Latitude ?? agency.Location.Latitude,
                request.Longitude ?? agency.Location.Longitude),
            Description.Create(request.Description ?? agency.Description?.Value)!,
            Email.Create(request.Email ?? request.Email ?? agency.Email?.Value)!,
            WebsiteUrl.Create(request.WebsiteUrl ?? agency.WebsiteUrl?.Value)!,
            TimeRange.Create(
                request.WorkStartTime ?? agency.WorkHour?.Start,
                request.WorkEndTime ?? agency.WorkHour?.End),
            SocialMedia.Create(
                request.Twitter ?? agency.SocialMedia?.Twitter,
                request.Facebook ?? agency.SocialMedia?.Facebook,
                request.Instagram ?? agency.SocialMedia?.Instagram,
                request.Linkedin ?? agency.SocialMedia?.Linkedin),
            PhoneNumber.Create(request.PhoneNumber ?? agency.PhoneNumber?.Value)!,
            request.LanguageIds);

        if (request.Image?.Length > 0)
        {
            var oldImageUrl = agency.ImageUrl;

            var cloudinaryImgUrl = await _imageService.UploadImageOnCloudinary(request.Image);

            if (oldImageUrl is not null)
            {
                _imageToDeleteRepository.Add(oldImageUrl.Value, _dateTimeProvider.Now);
            }
            var imageUrl = ImageUrl.Create(cloudinaryImgUrl);
            agency.UpdateImage(imageUrl.Value);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();

    }
}