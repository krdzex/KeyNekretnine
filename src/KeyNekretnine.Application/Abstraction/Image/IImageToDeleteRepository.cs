namespace KeyNekretnine.Application.Abstraction.Image;
public interface IImageToDeleteRepository
{
    Task AddAsync(string imageUrl, DateTime addedOnTime, CancellationToken cancellationToken = default);
    Task AddMultipleAsync(List<string> imageUrls, DateTime addedOnTime, CancellationToken cancellationToken = default);
}
