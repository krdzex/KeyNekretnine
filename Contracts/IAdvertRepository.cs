using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Contracts;
public interface IAdvertRepository
{
    //Task CreateAdvert(AddAdvertDto newAdvert, string userId);
    Task<AllInfomrationsAboutAdvertDto> GetAdvert(int advertId);
    Task<Pagination<MinimalInformationsAboutAdvertDto>> GetAdverts(AdvertParameters advertParameters);
    Task<IEnumerable<ShowAdvertLocationOnMapDto>> GetMapPoints(CancellationToken cancellationToken);
    Task<MinimalInformationsAboutAdvertDto> GetAdvertFromMapPoint(int id, CancellationToken cancellationToken);
}
