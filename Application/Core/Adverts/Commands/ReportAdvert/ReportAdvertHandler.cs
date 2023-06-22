using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Shared.Error;

namespace Application.Core.Adverts.Commands.ReportAdvert;
internal sealed class ReportAdvertHandler : ICommandHandler<ReportAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public ReportAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(ReportAdvertCommand request, CancellationToken cancellationToken)
    {
        var advertExist = await _repository.Advert.ChackIfAdvertExistAndItsApproved(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            return Result.Failure<Unit>(DomainErrors.Advert.AdvertNotFound(request.AdvertId));
        }

        var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail, cancellationToken);

        if (userId is null)
        {
            return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
        }

        var isReported = await _repository.Advert.ChackIfAdvertWithThisReasonUserAlreadyReported(userId, request.AdvertId, request.RejectReasonId, cancellationToken);

        if (isReported)
        {
            return Unit.Value;
        }

        await _repository.Advert.ReportAdvert(userId, request.AdvertId, request.RejectReasonId, cancellationToken);

        return Unit.Value;
    }
}