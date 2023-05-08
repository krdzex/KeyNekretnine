namespace Contracts;
public interface IAdvertFeatureRepository
{
    Task InsertFeature(string featureName, int advertId, CancellationToken cancellationToken);
}
