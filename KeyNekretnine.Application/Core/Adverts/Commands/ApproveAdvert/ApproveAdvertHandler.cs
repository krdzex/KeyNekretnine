using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
internal sealed class ApproveAdvertHandler : ICommandHandler<ApproveAdvertCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApproveAdvertHandler(
        IAdvertRepository advertRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(ApproveAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFoundForAdmin);
        }

        var result = advert.Approve(_dateTimeProvider.Now);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}