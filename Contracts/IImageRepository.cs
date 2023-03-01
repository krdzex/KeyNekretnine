namespace Contracts;
public interface IImageRepository
{
    Task InsertImages(IEnumerable<string> urls, int advertId, CancellationToken cancellationToken);
}