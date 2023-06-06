namespace Contracts;
public interface IImageRepository
{
    Task InsertImages(IEnumerable<string> urls, int advertId, CancellationToken cancellationToken);
    Task<List<string>> DeleteImagesAndGetPublicIds(IEnumerable<string> urls, int advertId, CancellationToken cancellationToken);
    Task<int> GetNumberOfImages(int advertId, CancellationToken cancellationToken);
}