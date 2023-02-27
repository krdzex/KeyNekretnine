using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Contracts;
public interface IAdvertRepository
{
    Task<AllInfomrationsAboutAdvertDto> GetAdvert(int advertId);
    Task<Pagination<MinimalInformationsAboutAdvertDto>> GetAdverts(AdvertParameters advertParameters);
    Task<IEnumerable<ShowAdvertLocationOnMapDto>> GetMapPoints(CancellationToken cancellationToken);
    Task<MinimalInformationsAboutAdvertDto> GetAdvertFromMapPoint(int id, CancellationToken cancellationToken);
    Task<int> CreateAdvert(AddAdvertDto newAdvert, string userId);
    Task UpdateAdvertCoverImage(string imageUrl, int advertId);
    Task UpdateStatus(int advertId);
    Task<bool> ChackIfAdvertExist(int advertId);
    Task ApproveAdvert(int advertId);
    Task DeclineAdvert(int advertId);
}
