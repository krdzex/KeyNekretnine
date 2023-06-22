using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Shared.Error;

namespace Application.Core.Adverts.Commands.ApproveAdvert;
internal sealed class ApproveAdvertHandler : ICommandHandler<ApproveAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IPublisher _publisher;

    public ApproveAdvertHandler(IRepositoryManager repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Result<Unit>> Handle(ApproveAdvertCommand request, CancellationToken cancellationToken)
    {

        var advertExist = await _repository.Advert.ChackIfAdvertExist(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            return Result.Failure<Unit>(DomainErrors.Advert.AdvertNotFound(request.AdvertId));
        }

        await _repository.Advert.ApproveAdvert(request.AdvertId, cancellationToken);

        var userEmail = await _repository.Advert.GetUserEmailFromAdvertId(request.AdvertId, cancellationToken);

        await _publisher.Publish(new AdvertApprovedEvent(userEmail, request.AdvertId), cancellationToken);

        return Unit.Value;
    }
}