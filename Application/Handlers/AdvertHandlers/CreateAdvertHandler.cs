using Application.Commands.AdvertCommands;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Service.Contracts;
using Shared;
using System.Transactions;

namespace Application.Handlers.AdvertHandlers;
internal sealed class CreateAdvertHandler : IRequestHandler<CreateAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IProcessingChannel _channel;

    public CreateAdvertHandler(IRepositoryManager repository, IProcessingChannel channel)
    {
        _repository = repository;
        _channel = channel;
    }
    public async Task<Unit> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        var advertId = -1;

        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail);

            if (userId is null)
            {
                throw new UserNotFoundException();
            }

            advertId = await _repository.Advert.CreateAdvert(request.AdvertForCreating, userId, cancellationToken);

            await _repository.TemporeryImageData.Insert(request.AdvertForCreating.CoverImage, advertId, true, cancellationToken);

            foreach (var image in request.AdvertForCreating.ImageFiles)
            {
                await _repository.TemporeryImageData.Insert(image, advertId, false, cancellationToken);
            }
            transaction.Complete();
        }

        if (advertId != -1)
        {
            await _channel.AddQueueItemAsync(new QueueItem { AdvertId = advertId });
        }
        return Unit.Value;
    }
}

