using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ReportAdvert;
internal sealed class ReportAdvertHandler : ICommandHandler<ReportAdvertCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    public ReportAdvertHandler(
        IAdvertRepository advertRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _advertRepository = advertRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ReportAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdWithReportsAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFoundForAdmin);
        }

        var reportResult = advert.Report(request.UserId, request.RejectReasonId, _dateTimeProvider.Now);

        if (reportResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}