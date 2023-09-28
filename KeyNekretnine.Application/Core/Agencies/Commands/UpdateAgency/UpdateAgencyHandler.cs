using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Shared;
using MediatR;

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
            return Result.Failure<Unit>(AgencyErrors.NotFound);
        }

        if (agency.UserId != request.UserId)
        {
            return Result.Failure<Unit>(AgencyErrors.NotOwner);

        }

        var workHour = TimeRange.Create(request.WorkStartTime, request.WorkEndTime);

        agency.Update(
            new Name(request.Name),
            new Location(
                request.Address,
                request.Latitude,
                request.Longitude),
            new Description(request.Description),
            new Email(request.Email),
            new WebsiteUrl(request.WebsiteUrl),
            workHour,
            new SocialMedia(
                request.Twitter,
                request.Facebook,
                request.Instagram,
                request.Linkedin),
            new PhoneNumber(request.PhoneNumber),
            request.LanguageIds);

        if (request.Image?.Length > 0)
        {
            var oldImageUrl = agency.ImageUrl;

            var imageUrl = await _imageService.UploadImageOnCloudinary(request.Image);

            if (oldImageUrl is not null)
            {
                _imageToDeleteRepository.Add(oldImageUrl.Value, _dateTimeProvider.Now);
            }
            agency.UpdateImage(new ImageUrl(imageUrl));
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();

    }
}