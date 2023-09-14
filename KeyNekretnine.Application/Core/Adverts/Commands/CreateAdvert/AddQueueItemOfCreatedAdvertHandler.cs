using MediatR;
using Service.Contracts;
using Shared;

namespace KeyNekretnine.Application.Core.Adverts.Commands.CreateAdvert;
internal sealed class AddQueueItemOfCreatedAdvertHandler : INotificationHandler<AdvertCreatedEvent>
{
    private readonly IProcessingChannel _channel;

    public AddQueueItemOfCreatedAdvertHandler(IProcessingChannel channel)
    {
        _channel = channel;
    }

    public async Task Handle(AdvertCreatedEvent notification, CancellationToken cancellationToken)
    {

        if (notification.AdvertId != -1)
        {
            await _channel.AddQueueItemAsync(new QueueItem { AdvertId = notification.AdvertId }, cancellationToken);
        }

        await Task.CompletedTask;
    }
}