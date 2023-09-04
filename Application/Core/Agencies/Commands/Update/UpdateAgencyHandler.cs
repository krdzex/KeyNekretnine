using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;
using MediatR;

namespace KeyNekretnine.Application.Core.Agencies.Commands.Update;
internal sealed class UpdateAgencyHandler : ICommandHandler<UpdateAgencyCommand>
{
    private readonly IAgencyRepository _agencyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;
    public UpdateAgencyHandler(IAgencyRepository agencyRepository, IUnitOfWork unitOfWork, IImageService imageService)
    {
        _agencyRepository = agencyRepository;
        _unitOfWork = unitOfWork;
        _imageService = imageService;
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
            request.LanguageIds);


        var imageUrl = await _imageService.UploadImageOnCloudinary(request.Image);

        //Dodati dio gdje dodajemo stari image url u bazu za brisanje slika
        var oldImageUrl = agency.ImageUrl;

        agency.UpdateImageUrl(new ImageUrl(imageUrl));

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();

    }
}