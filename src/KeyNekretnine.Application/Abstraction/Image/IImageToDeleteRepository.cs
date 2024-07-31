namespace KeyNekretnine.Application.Abstraction.Image;
public interface IImageToDeleteRepository
{
    Task AddAsync(string imageUrl, DateTime addedOnTime, CancellationToken cancellationToken = default);
}
