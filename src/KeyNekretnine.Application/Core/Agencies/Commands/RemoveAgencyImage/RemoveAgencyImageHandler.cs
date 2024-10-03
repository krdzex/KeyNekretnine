using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;

namespace KeyNekretnine.Application.Core.Agencies.Commands.RemoveAgencyImage;
internal sealed class RemoveAgencyImageHandler : ICommandHandler<RemoveAgencyImageCommand>
{
    private readonly IAgencyRepository _agencyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageToDeleteRepository _imageToDeleteRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserContext _userContext;

    public RemoveAgencyImageHandler(
        IAgencyRepository agencyRepository,
        IUnitOfWork unitOfWork,
        IImageToDeleteRepository imageToDeleteRepository,
        IDateTimeProvider dateTimeProvider,
        IUserContext userContext)
    {
        _agencyRepository = agencyRepository;
        _unitOfWork = unitOfWork;
        _imageToDeleteRepository = imageToDeleteRepository;
        _dateTimeProvider = dateTimeProvider;
        _userContext = userContext;
    }

    public async Task<Result> Handle(RemoveAgencyImageCommand request, CancellationToken cancellationToken)
    {
        var agency = await _agencyRepository.GetByIdAsync(request.AgencyId, cancellationToken);

        if (agency is null)
        {
            return Result.Failure(AgencyErrors.NotFound);
        }

        if (agency.UserId != _userContext.UserId)
        {
            return Result.Failure(AgencyErrors.NotOwner);
        }

        if (agency.ImageUrl != null)
        {
            await _imageToDeleteRepository.AddAsync(agency.ImageUrl.Value, _dateTimeProvider.Now, cancellationToken);

            agency.UpdateImage(null);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}