﻿using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Contracts;
public interface IAdvertRepository
{
    Task<AllInfomrationsAboutAdvertDto> GetAdvert(int advertId, CancellationToken cancellationToken);
    Task<AdminAllInformationsAboutAdvertDto> GetAdminAdvert(int advertId, CancellationToken cancellationToken);
    Task<Pagination<MinimalInformationsAboutAdvertDto>> GetAdverts(AdvertParameters advertParameters, CancellationToken cancellationToken);
    Task<IEnumerable<ShowAdvertLocationOnMapDto>> GetMapPoints(CancellationToken cancellationToken);
    Task<MinimalInformationsAboutAdvertDto> GetAdvertFromMapPoint(int id, CancellationToken cancellationToken);
    Task<int> CreateAdvert(AddAdvertDto newAdvert, string userId, CancellationToken cancellationToken);
    Task UpdateAdvertCoverImage(string imageUrl, int advertId, CancellationToken cancellationToken);
    Task UpdateStatus(int advertId, CancellationToken cancellationToken);
    Task<bool> ChackIfAdvertExist(int advertId, CancellationToken cancellationToken);
    Task<bool> ChackIfAdvertExistAndItsApproved(int advertId, CancellationToken cancellationToken);
    Task ApproveAdvert(int advertId, CancellationToken cancellationToken);
    Task DeclineAdvert(int advertId, CancellationToken cancellationToken);
    Task<Pagination<AdminTableAdvertDto>> GetAdminAdverts(AdminAdvertParameters adminAdvertParameters, CancellationToken cancellationToken);
    Task<Pagination<MyAdvertsDto>> GetMyAdverts(MyAdvertsParameters MyAdvertParameters, string userId, CancellationToken cancellationToken);
    Task<string> GetUserEmailFromAdvertId(int advertId, CancellationToken cancellationToken);
    Task MakeAdvertFavorite(string userId, int advertId, CancellationToken cancellationToken);
    Task<bool> ChackIfAdvertIsFavorite(string userId, int advertId, CancellationToken cancellationToken);
    Task RemoveAdvertFromFavorite(string userId, int advertId, CancellationToken cancellationToken);
    Task<Pagination<MinimalInformationsAboutAdvertDto>> GetFavoriteAdverts(FavoriteAdvertsParameters requestParameters, string userId, CancellationToken cancellationToken);
    Task<bool> ChackIfAdvertWithThisReasonUserAlreadyReported(string userId, int advertId, int reasonId, CancellationToken cancellationToken);
    Task ReportAdvert(string userId, int advertId, int reasonId, CancellationToken cancellationToken);
    Task<Pagination<AdvertReportsDto>> GetAdvertReports(ReportParameters reportParameters, CancellationToken cancellationToken);
    Task<IEnumerable<CompareAdvertDto>> GetAdvertsCompare(int firstAdvert, int sacondAdvert, CancellationToken cancellationToken);
    Task UpdateAdvertInformations(UpdateAdvertInformationsDto updateAdvertInformationsDto, int advertId, CancellationToken cancellationToken);
    Task<bool> ChackIfUserIsAdvertOwner(int advertId, string email);
    Task UpdateAdvertLocation(UpdateAdvertLocationDto updateAdvertLocationDto, int advertId, CancellationToken cancellationToken);
    Task<MyAdvertDto> GetMyAdvert(int advertId, string userId, CancellationToken cancellationToken);
}