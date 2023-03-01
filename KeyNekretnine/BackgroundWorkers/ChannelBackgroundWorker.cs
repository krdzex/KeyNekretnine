using Contracts;
using Service.Contracts;
using Shared;
using System.Transactions;

namespace KeyNekretnine.BackgroundWorkers;
public class ChannelBackgroundWorker : BackgroundService
{
    private readonly IProcessingChannel _checkoutProcessingChannel;
    private readonly IServiceProvider _serviceProvider;

    public ChannelBackgroundWorker(IProcessingChannel checkoutProcessingChannel,
                                   IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _checkoutProcessingChannel = checkoutProcessingChannel;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _checkoutProcessingChannel.TryCompleteWriter();
        await base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await foreach (var item in _checkoutProcessingChannel.ReadAllAsync(cancellationToken))
        {
            await ProcessItemAsync(item, cancellationToken);
        }
    }

    private async Task ProcessItemAsync(QueueItem item, CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var scopedServiceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
            var scopedRepositoryManager = scope.ServiceProvider.GetRequiredService<IRepositoryManager>();

            using (var transaction = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled))
            {
                var coverImageData = await scopedRepositoryManager.TemporeryImageData.Get(item.AdvertId, true, cancellationToken);
                var advertImagesData = await scopedRepositoryManager.TemporeryImageData.Get(item.AdvertId, false, cancellationToken);

                var coverImageUrl = await scopedServiceManager.ImageService.UploadImageOnCloudinary(coverImageData.First());

                var imagesUrls = new List<string>();

                foreach (var imageData in advertImagesData)
                {
                    var url = await scopedServiceManager.ImageService.UploadImageOnCloudinary(imageData);
                    imagesUrls.Add(url);
                }

                await scopedRepositoryManager.Advert.UpdateAdvertCoverImage(coverImageUrl, item.AdvertId, cancellationToken);
                await scopedRepositoryManager.Image.InsertImages(imagesUrls, item.AdvertId, cancellationToken);

                await scopedRepositoryManager.TemporeryImageData.DeleteAll(item.AdvertId, cancellationToken);
                await scopedRepositoryManager.Advert.UpdateStatus(item.AdvertId, cancellationToken);

                transaction.Complete();
            }
        }
    }
}
