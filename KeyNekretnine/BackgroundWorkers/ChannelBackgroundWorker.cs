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

            var coverImageUrl = await scopedServiceManager.ImageService.UploadImageOnCloudinary(item.CoverImagePath);
            var imageUrls = await scopedServiceManager.ImageService.UploadMultipleImagesOnCloudinary(item.ImagePaths);

            await scopedRepositoryManager.Advert.UpdateAdvertCoverImage(coverImageUrl, item.AdvertId);
            await scopedRepositoryManager.Image.InsertImages(imageUrls, item.AdvertId);
        }
    }
}
