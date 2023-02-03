﻿using Application.Commands.AdvertCommands;
using Contracts;
using MediatR;
using Service.Contracts;
using Shared;

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
        var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail);

        if (userId is null)
        {

        }
        var advertId = await _repository.Advert.CreateAdvert(request.AdvertForCreating, userId);

        await _repository.TemporeryImageData.Insert(request.AdvertForCreating.CoverImage, advertId, true);

        foreach (var image in request.AdvertForCreating.ImageFiles)
        {
            await _repository.TemporeryImageData.Insert(image, advertId, false);
        }

        var addToChannel = await _channel.AddQueueItemAsync(new QueueItem { AdvertId = advertId });

        return Unit.Value;
    }
}

