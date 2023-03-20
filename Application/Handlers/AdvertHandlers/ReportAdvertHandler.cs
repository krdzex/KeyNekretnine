using Application.Commands.AdvertCommands;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class ReportAdvertHandler : IRequestHandler<ReportAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public ReportAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(ReportAdvertCommand request, CancellationToken cancellationToken)
    {
        var advertExist = await _repository.Advert.ChackIfAdvertExistAndItsApproved(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            throw new AdvertNotFoundException(request.AdvertId);
        }

        var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail, cancellationToken);

        var isReported = await _repository.Advert.ChackIfAdvertWithThisReasonUserAlreadyReported(userId, request.AdvertId, request.RejectReasonId, cancellationToken);

        if (isReported)
        {
            return Unit.Value;
        }

        await _repository.Advert.ReportAdvert(userId, request.AdvertId, request.RejectReasonId, cancellationToken);

        return Unit.Value;
    }
}

