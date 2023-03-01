using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Contracts;
public interface IAdvertRepository
{
    Task<AllInfomrationsAboutAdvertDto> GetAdvert(int advertId);
    Task<AdminAllInformationsAboutAdvertDto> GetAdminAdvert(int advertId);
    Task<Pagination<MinimalInformationsAboutAdvertDto>> GetAdverts(AdvertParameters advertParameters, CancellationToken cancellationToken);
    Task<IEnumerable<ShowAdvertLocationOnMapDto>> GetMapPoints(CancellationToken cancellationToken);
    Task<MinimalInformationsAboutAdvertDto> GetAdvertFromMapPoint(int id, CancellationToken cancellationToken);
    Task<int> CreateAdvert(AddAdvertDto newAdvert, string userId, CancellationToken cancellationToken);
    Task UpdateAdvertCoverImage(string imageUrl, int advertId, CancellationToken cancellationToken);
    Task UpdateStatus(int advertId, CancellationToken cancellationToken);
    Task<bool> ChackIfAdvertExist(int advertId, CancellationToken cancellationToken);
    Task ApproveAdvert(int advertId, CancellationToken cancellationToken);
    Task DeclineAdvert(int advertId, CancellationToken cancellationToken);
    Task<Pagination<AdminTableAdvertDto>> GetAdminAdverts(AdminAdvertParameters adminAdvertParameters, CancellationToken cancellationToken);
}
