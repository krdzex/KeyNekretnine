using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
internal sealed class RejectAdvertHandler : ICommandHandler<RejectAdvertCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RejectAdvertHandler(
        IAdvertRepository advertRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(RejectAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFoundForAdmin);
        }

        var result = advert.Reject(_dateTimeProvider.Now);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}