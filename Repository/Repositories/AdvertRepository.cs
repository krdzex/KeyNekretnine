﻿using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.DataTransferObjects.AdvertFeature;
using Shared.DataTransferObjects.Image;
using Shared.RequestFeatures;
using System.Data;

namespace Repository.Repositories;

internal class AdvertRepository : IAdvertRepository
{
    private readonly DapperContext _dapperContext;
    public AdvertRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<int> CreateAdvert(AddAdvertDto newAdvert, string userId, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.AddAdvertQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var referenceId = new Random().Next().ToString("x");
            var param = new DynamicParameters();

            param.Add("price", newAdvert.Price, DbType.Double);
            param.Add("description_sr", newAdvert.DescriptionSr, DbType.String);
            param.Add("description_en", newAdvert.DescriptionEn, DbType.String);
            param.Add("floor_space", newAdvert.FloorSpace, DbType.Double);
            param.Add("street", newAdvert.Street, DbType.String);
            param.Add("no_of_bedrooms", newAdvert.NoOfBedrooms, DbType.Int16);
            param.Add("no_of_bathrooms", newAdvert.NoOfBathrooms, DbType.Int16);
            param.Add("has_elevator", newAdvert.HasElevator, DbType.Boolean);
            param.Add("has_garage", newAdvert.HasGarage, DbType.Boolean);
            param.Add("has_terrace", newAdvert.HasTerrace, DbType.Boolean);
            param.Add("latitude", newAdvert.Latitude, DbType.Double);
            param.Add("longitude", newAdvert.Longitude, DbType.Double);
            param.Add("has_wifi", newAdvert.HasWifi, DbType.Boolean);
            param.Add("is_furnished", newAdvert.IsFurnished, DbType.Boolean);
            param.Add("created_date", DateTime.Now, DbType.DateTime);
            param.Add("year_of_building_created", newAdvert.YearOfBuildingCreated, DbType.Int16);
            param.Add("cover_image_url", "test", DbType.String);
            param.Add("neighborhood_id", newAdvert.NeighborhoodId, DbType.Int16);
            param.Add("building_floor", newAdvert.BuildingFloor, DbType.Int16);
            param.Add("purpose_id", newAdvert.AdvertPurposeId, DbType.Int16);
            param.Add("type_id", newAdvert.AdvertTypeId, DbType.Int16);
            param.Add("user_id", userId, DbType.String);
            param.Add("reference_id", referenceId, DbType.String);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var advertId = await connection.QuerySingleAsync<int>(cmd);

            return advertId;
        }
    }

    public async Task<AllInfomrationsAboutAdvertDto> GetAdvert(int advertId, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.SingleAdvertQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("id", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);

            var advert = await multi.ReadSingleOrDefaultAsync<AllInfomrationsAboutAdvertDto>();

            if (advert != null)
            {
                advert.Images = (await multi.ReadAsync<ImageDto>()).ToList();
                advert.Images.Insert(0, new ImageDto { Url = advert.Cover_Image_Url });

                advert.Features = (await multi.ReadAsync<FeatureDto>()).ToList();
            }

            return advert;
        }
    }

    public async Task<AdminAllInformationsAboutAdvertDto> GetAdminAdvert(int advertId, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.SingleAdminAdvertQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("id", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);

            var advert = await multi.ReadSingleOrDefaultAsync<AdminAllInformationsAboutAdvertDto>();

            if (advert != null)
            {
                advert.Images = (await multi.ReadAsync<ImageDto>()).ToList();
                advert.Images.Insert(0, new ImageDto { Url = advert.Cover_Image_Url });

                advert.Features = (await multi.ReadAsync<FeatureDto>()).ToList();
            }

            return advert;
        }
    }

    public async Task<IEnumerable<ShowAdvertLocationOnMapDto>> GetMapPoints(CancellationToken cancellationToken)
    {
        var query = AdvertQuery.AllAdvertMapPoints;

        using (var connection = _dapperContext.CreateConnection())
        {
            var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

            var mapPoints = await connection.QueryAsync<ShowAdvertLocationOnMapDto>(cmd);

            return mapPoints;
        }
    }

    public async Task<MinimalInformationsAboutAdvertDto> GetAdvertFromMapPoint(int id, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.SingleAdvertForMapPoint;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("id", id, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var mapAdvert = await connection.QuerySingleOrDefaultAsync<MinimalInformationsAboutAdvertDto>(cmd);

            return mapAdvert;
        }
    }

    public async Task<Pagination<MinimalInformationsAboutAdvertDto>> GetAdverts(AdvertParameters advertParameters, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<MinimalInformationsAboutAdvertDto>(advertParameters.OrderBy, 'a');

        var query = AdvertQuery.MakeGetAdvertQuery(advertParameters, orderBy);

        var skip = (advertParameters.PageNumber - 1) * advertParameters.PageSize;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("skip", skip, DbType.Int32);
            param.Add("take", advertParameters.PageSize, DbType.Int32);
            param.Add("minPrice", advertParameters.MinPrice, DbType.Int32);
            param.Add("maxPrice", advertParameters.MaxPrice, DbType.Int32);
            param.Add("noOfBedrooms", advertParameters.NoOfBedrooms);
            param.Add("noOfBathrooms", advertParameters.NoOfBathrooms);
            param.Add("typeIds", advertParameters.AdvertTypeIds);
            param.Add("purposeIds", advertParameters.AdvertPurposeIds);
            param.Add("cityId", advertParameters.CityId, DbType.Int32);
            param.Add("neighborhoodIds", advertParameters.NeighborhoodIds);
            param.Add("minFloorSpace", advertParameters.MinFloorSpace, DbType.Int32);
            param.Add("maxFloorSpace", advertParameters.MaxFloorSpace, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);

            var count = await multi.ReadSingleAsync<int>();
            var adverts = (await multi.ReadAsync<MinimalInformationsAboutAdvertDto>()).ToList();

            var metadata = new PagedList<MinimalInformationsAboutAdvertDto>(adverts, count, advertParameters.PageNumber, advertParameters.PageSize);

            return new Pagination<MinimalInformationsAboutAdvertDto> { Data = adverts, MetaData = metadata.MetaData };
        }
    }

    public async Task UpdateAdvertCoverImage(string imageUrl, int advertId, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.UpdateCoverImageQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("coverImageUrl", imageUrl, DbType.String);
            param.Add("advertId", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task UpdateStatus(int advertId, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.UpdateAdvertStatus;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("advertId", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<bool> ChackIfAdvertExist(int advertId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.AdvertExistQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("advertId", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            int count = await connection.QueryFirstOrDefaultAsync<int>(cmd);
            return count > 0;
        }
    }

    public async Task ApproveAdvert(int advertId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.ApproveAdvertQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("advertId", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task DeclineAdvert(int advertId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.DeclineAdvertQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("advertId", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<Pagination<AdminTableAdvertDto>> GetAdminAdverts(AdminAdvertParameters adminAdvertParameters, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<MinimalInformationsAboutAdvertDto>(adminAdvertParameters.OrderBy, 'a');

        var query = AdvertQuery.MakeGetAdminAdvertQuery(adminAdvertParameters.AdvertStatusIds, orderBy);

        var skip = (adminAdvertParameters.PageNumber - 1) * adminAdvertParameters.PageSize;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("skip", skip, DbType.Int32);
            param.Add("take", adminAdvertParameters.PageSize, DbType.Int32);
            param.Add("advertStatusIds", adminAdvertParameters.AdvertStatusIds);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);

            var count = await multi.ReadSingleAsync<int>();
            var adverts = (await multi.ReadAsync<AdminTableAdvertDto>()).ToList();

            var metadata = new PagedList<AdminTableAdvertDto>(adverts, count, adminAdvertParameters.PageNumber, adminAdvertParameters.PageSize);

            return new Pagination<AdminTableAdvertDto> { Data = adverts, MetaData = metadata.MetaData };
        }
    }

    public async Task<Pagination<MinimalInformationsAboutAdvertDto>> GetMyAdverts(MyAdvertsParameters myAdvertParameters, string userId, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<MinimalInformationsAboutAdvertDto>(myAdvertParameters.OrderBy, 'a');

        var query = AdvertQuery.MakeGetMyAdvertQuery(myAdvertParameters, orderBy, userId);

        var skip = (myAdvertParameters.PageNumber - 1) * myAdvertParameters.PageSize;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("skip", skip, DbType.Int32);
            param.Add("take", myAdvertParameters.PageSize, DbType.Int32);
            param.Add("minPrice", myAdvertParameters.MinPrice, DbType.Int32);
            param.Add("maxPrice", myAdvertParameters.MaxPrice, DbType.Int32);
            param.Add("noOfBedrooms", myAdvertParameters.NoOfBedrooms);
            param.Add("noOfBathrooms", myAdvertParameters.NoOfBathrooms);
            param.Add("typeIds", myAdvertParameters.AdvertTypeIds);
            param.Add("purposeIds", myAdvertParameters.AdvertPurposeIds);
            param.Add("cityId", myAdvertParameters.CityId, DbType.Int32);
            param.Add("neighborhoodIds", myAdvertParameters.NeighborhoodIds);
            param.Add("minFloorSpace", myAdvertParameters.MinFloorSpace, DbType.Int32);
            param.Add("maxFloorSpace", myAdvertParameters.MaxFloorSpace, DbType.Int32);
            param.Add("userId", userId, DbType.String);
            param.Add("advertStatusIds", myAdvertParameters.AdvertStatusIds);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);

            var count = await multi.ReadSingleAsync<int>();
            var adverts = (await multi.ReadAsync<MinimalInformationsAboutAdvertDto>()).ToList();

            var metadata = new PagedList<MinimalInformationsAboutAdvertDto>(adverts, count, myAdvertParameters.PageNumber, myAdvertParameters.PageSize);

            return new Pagination<MinimalInformationsAboutAdvertDto> { Data = adverts, MetaData = metadata.MetaData };
        }
    }

    public async Task<string> GetUserEmailFromAdvertId(int advertId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.GetUserEmailFromAdvertIdQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("advertId", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            return await connection.QuerySingleOrDefaultAsync<string>(cmd);
        }
    }

    public async Task MakeAdvertFavorite(string userId, int advertId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.MakeAdvertFavoriteQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("@advertId", advertId, DbType.Int32);
            param.Add("@userId", userId, DbType.String);
            param.Add("createdFavoriteDate", DateTime.Now, DbType.DateTime);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<bool> ChackIfAdvertExistAndItsApproved(int advertId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.AdvertExistAndApprovedQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("advertId", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            int count = await connection.QueryFirstOrDefaultAsync<int>(cmd);
            return count > 0;
        }
    }

    public async Task<bool> ChackIfAdvertIsFavorite(string userId, int advertId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.ChackIfAdvertIsFavoriteQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("advertId", advertId, DbType.Int32);
            param.Add("userId", userId, DbType.String);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            int count = await connection.QueryFirstOrDefaultAsync<int>(cmd);
            return count > 0;
        }
    }

    public async Task RemoveAdvertFromFavorite(string userId, int advertId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.DeleteAdvertFromFavoriteQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("@advertId", advertId, DbType.Int32);
            param.Add("@userId", userId, DbType.String);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<Pagination<MinimalInformationsAboutAdvertDto>> GetFavoriteAdverts(FavoriteAdvertsParameters requestParameters, string userId, CancellationToken cancellationToken)
    {

        var orderBy = OrderQueryBuilder.CreateOrderQuery<MinimalInformationsAboutAdvertDto>(requestParameters.OrderBy, 'a');

        string query = AdvertQuery.GetFavoriteAdverts;

        var skip = (requestParameters.PageNumber - 1) * requestParameters.PageSize;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("skip", skip, DbType.Int32);
            param.Add("take", requestParameters.PageSize, DbType.Int32);
            param.Add("orderBy", orderBy, DbType.String);
            param.Add("userId", userId, DbType.String);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);

            var count = await multi.ReadSingleAsync<int>();
            var adverts = (await multi.ReadAsync<MinimalInformationsAboutAdvertDto>()).ToList();

            var metadata = new PagedList<MinimalInformationsAboutAdvertDto>(adverts, count, requestParameters.PageNumber, requestParameters.PageSize);

            return new Pagination<MinimalInformationsAboutAdvertDto> { Data = adverts, MetaData = metadata.MetaData };
        }
    }

    public async Task ReportAdvert(string userId, int advertId, int reasonId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.ReportAdvertQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("advertId", advertId, DbType.Int32);
            param.Add("userId", userId, DbType.String);
            param.Add("rejectReasonId", reasonId, DbType.Int32);
            param.Add("createdReportDate", DateTime.Now, DbType.DateTime);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<bool> ChackIfAdvertWithThisReasonUserAlreadyReported(string userId, int advertId, int reasonId, CancellationToken cancellationToken)
    {
        string query = AdvertQuery.ChackIfAdvertIsAlreadyReportedQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("advertId", advertId, DbType.Int32);
            param.Add("userId", userId, DbType.String);
            param.Add("rejectReasonId", reasonId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            int count = await connection.QueryFirstOrDefaultAsync<int>(cmd);
            return count > 0;
        }
    }

    public async Task<Pagination<AdvertReportsDto>> GetAdvertReports(ReportParameters reportParameters, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<AdvertReportsDto>(reportParameters.OrderBy, 'a');

        var query = AdvertQuery.GetReportsQuery;

        var skip = (reportParameters.PageNumber - 1) * reportParameters.PageSize;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("skip", skip, DbType.Int32);
            param.Add("take", reportParameters.PageSize, DbType.Int32);
            param.Add("orderBy", orderBy, DbType.String);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);

            var count = await multi.ReadSingleAsync<int>();
            var reports = (await multi.ReadAsync<AdvertReportsDto>()).ToList();

            var metadata = new PagedList<AdvertReportsDto>(reports, count, reportParameters.PageNumber, reportParameters.PageSize);

            return new Pagination<AdvertReportsDto> { Data = reports, MetaData = metadata.MetaData };
        }
    }

    public async Task<IEnumerable<CompareAdvertDto>> GetAdvertsCompare(int firstAdvert, int sacondAdvert, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.GetCompareAdvertsQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var advertsId = new List<int> { firstAdvert, sacondAdvert };

            var param = new DynamicParameters();

            param.Add("@advertsId", advertsId);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var adverts = await connection.QueryAsync<CompareAdvertDto>(cmd);

            return adverts;
        }
    }

    public async Task UpdateAdvertInformations(UpdateAdvertInformationsDto updateAdvertInformationsDto, int advertId, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.UpdateAdvertInformationsQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("price", updateAdvertInformationsDto.Price, DbType.Double);
            param.Add("description_sr", updateAdvertInformationsDto.DescriptionSr, DbType.String);
            param.Add("description_en", updateAdvertInformationsDto.DescriptionEn, DbType.String);
            param.Add("floor_space", updateAdvertInformationsDto.FloorSpace, DbType.Double);
            param.Add("no_of_bedrooms", updateAdvertInformationsDto.NoOfBedrooms, DbType.Int16);
            param.Add("no_of_bathrooms", updateAdvertInformationsDto.NoOfBathrooms, DbType.Int16);
            param.Add("has_elevator", updateAdvertInformationsDto.HasElevator, DbType.Boolean);
            param.Add("has_garage", updateAdvertInformationsDto.HasGarage, DbType.Boolean);
            param.Add("has_terrace", updateAdvertInformationsDto.HasTerrace, DbType.Boolean);
            param.Add("has_wifi", updateAdvertInformationsDto.HasWifi, DbType.Boolean);
            param.Add("is_furnished", updateAdvertInformationsDto.IsFurnished, DbType.Boolean);
            param.Add("year_of_building_created", updateAdvertInformationsDto.YearOfBuildingCreated, DbType.Int16);
            param.Add("building_floor", updateAdvertInformationsDto.BuildingFloor, DbType.Int16);
            param.Add("purpose_id", updateAdvertInformationsDto.AdvertPurposeId, DbType.Int16);
            param.Add("type_id", updateAdvertInformationsDto.AdvertTypeId, DbType.Int16);
            param.Add("advertId", advertId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<bool> ChackIfUserIsAdvertOwner(int advertId, string email)
    {
        string query = AdvertQuery.ChackIfUserIsAdvertOwnerQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("email", email, DbType.String);
            param.Add("advertId", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param);

            int count = await connection.QueryFirstOrDefaultAsync<int>(cmd);
            return count > 0;
        }
    }

    public async Task UpdateAdvertLocation(UpdateAdvertLocationDto updateAdvertLocationDto, int advertId, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.UpdateAdvertLocationQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("latitude", updateAdvertLocationDto.Latitude, DbType.Double);
            param.Add("longitude", updateAdvertLocationDto.Longitude, DbType.Double);
            param.Add("street", updateAdvertLocationDto.Street, DbType.String);
            param.Add("neighborhoodId", updateAdvertLocationDto.NeighborhoodId, DbType.Int32);
            param.Add("advertId", advertId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }
}