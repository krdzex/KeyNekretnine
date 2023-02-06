using Contracts;
using Service.Contracts;
using Shared;

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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var item in _checkoutProcessingChannel.ReadAllAsync(stoppingToken))
        {
            await ProcessItemAsync(item);
        }
    }

    private async Task ProcessItemAsync(QueueItem item)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var scopedServiceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
            var scopedRepositoryManager = scope.ServiceProvider.GetRequiredService<IRepositoryManager>();

            var coverImageData = await scopedRepositoryManager.TemporeryImageData.Get(item.AdvertId, true);
            var advertImagesData = await scopedRepositoryManager.TemporeryImageData.Get(item.AdvertId, false);

            var coverImageUrl = await scopedServiceManager.ImageService.UploadImageOnCloudinary(coverImageData.First());

            var imagesUrls = new List<string>();

            foreach (var imageData in advertImagesData)
            {
                var url = await scopedServiceManager.ImageService.UploadImageOnCloudinary(imageData);
                imagesUrls.Add(url);
            }

            await scopedRepositoryManager.Advert.UpdateAdvertCoverImage(coverImageUrl, item.AdvertId);
            await scopedRepositoryManager.Image.InsertImages(imagesUrls, item.AdvertId);

            await scopedRepositoryManager.TemporeryImageData.DeleteAll(item.AdvertId);
            await scopedRepositoryManager.Advert.UpdateStatus(item.AdvertId);
        }
    }
}
