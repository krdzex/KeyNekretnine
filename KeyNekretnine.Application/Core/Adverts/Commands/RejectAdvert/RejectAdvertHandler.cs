using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
internal sealed class RejectAdvertHandler : ICommandHandler<RejectAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IPublisher _publisher;

    public RejectAdvertHandler(IRepositoryManager repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Result<Unit>> Handle(RejectAdvertCommand request, CancellationToken cancellationToken)
    {
        var advertExist = await _repository.Advert.ChackIfAdvertExist(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            return Result.Failure<Unit>(DomainErrors.Advert.AdvertNotFound(request.AdvertId));
        }

        await _repository.Advert.DeclineAdvert(request.AdvertId, cancellationToken);

        var userEmail = await _repository.Advert.GetUserEmailFromAdvertId(request.AdvertId, cancellationToken);

        await _publisher.Publish(new AdvertRejectedEvent(userEmail, request.AdvertId), cancellationToken);

        return Unit.Value;
    }
}