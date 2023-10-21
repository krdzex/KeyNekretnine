using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ReportAdvert;
internal sealed class ReportAdvertHandler : ICommandHandler<ReportAdvertCommand>
{
    private readonly IAdvertRepository _advertRepository;

    public ReportAdvertHandler(IAdvertRepository advertRepository)
    {
        _advertRepository = advertRepository;
    }

    public async Task<Result> Handle(ReportAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFoundForAdmin);
        }

        //var advertExist = await _repository.Advert.ChackIfAdvertExistAndItsApproved(request.AdvertId, cancellationToken);

        //if (!advertExist)
        //{
        //    return Result.Failure<Unit>(DomainErrors.Advert.AdvertNotFound(request.AdvertId));
        //}

        //var isReported = await _repository.Advert.ChackIfAdvertWithThisReasonUserAlreadyReported(userId, request.AdvertId, request.RejectReasonId, cancellationToken);

        //if (isReported)
        //{
        //    return Unit.Value;
        //}

        //await _repository.Advert.ReportAdvert(userId, request.AdvertId, request.RejectReasonId, cancellationToken);

        return Result.Success();
    }
}