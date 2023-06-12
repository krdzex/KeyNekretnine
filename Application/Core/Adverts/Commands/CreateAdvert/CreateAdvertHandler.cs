using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Shared.Error;
using System.Transactions;

namespace Application.Core.Adverts.Commands.CreateAdvert;
internal sealed class CreateAdvertHandler : ICommandHandler<CreateAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IPublisher _publisher;
    public CreateAdvertHandler(IRepositoryManager repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Result<Unit>> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        var advertId = -1;

        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail, cancellationToken);

            if (userId is null)
            {
                return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
            }

            advertId = await _repository.Advert.CreateAdvert(request.AdvertForCreating, userId, cancellationToken);

            await _repository.TemporeryImageData.Insert(request.AdvertForCreating.CoverImage, advertId, true, cancellationToken);

            foreach (var image in request.AdvertForCreating.ImageFiles)
            {
                await _repository.TemporeryImageData.Insert(image, advertId, false, cancellationToken);
            }

            foreach (var feature in request.AdvertForCreating.Features)
            {
                await _repository.AdvertFeature.InsertFeature(feature, advertId, cancellationToken);
            }

            transaction.Complete();
        }

        await _publisher.Publish(new AdvertCreatedEvent(advertId), cancellationToken);

        return Unit.Value;
    }
}

