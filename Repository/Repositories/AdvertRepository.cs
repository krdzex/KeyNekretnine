using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
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

    public async Task<int> CreateAdvert(AddAdvertDto newAdvert, string userId)
    {

        var query = AdvertQuery.AddAdvertQuery;

        var param = new DynamicParameters();
        param.Add("@price", newAdvert.Price, DbType.Double);
        param.Add("@description", newAdvert.Description, DbType.String);
        param.Add("@floor_space", newAdvert.FloorSpace, DbType.Double);
        param.Add("@street", newAdvert.Street, DbType.String);
        param.Add("@no_of_badrooms", newAdvert.NoOfBadrooms, DbType.Int16);
        param.Add("@no_of_bathrooms", newAdvert.NoOfBathrooms, DbType.Int16);
        param.Add("@has_elevator", newAdvert.HasElevator, DbType.Boolean);
        param.Add("@has_garage", newAdvert.HasGarage, DbType.Boolean);
        param.Add("@has_terrace", newAdvert.HasTerrace, DbType.Boolean);
        param.Add("@latitude", newAdvert.Latitude, DbType.Double);
        param.Add("@longitude", newAdvert.Longitude, DbType.Double);
        param.Add("@has_wifi", newAdvert.HasWifi, DbType.Boolean);
        param.Add("@is_furnished", newAdvert.IsFunished, DbType.Boolean);
        param.Add("@created_date", DateTime.Now, DbType.DateTime);
        param.Add("@year_of_building_created", newAdvert.YearOfBuildingCreated, DbType.Int16);
        param.Add("@cover_image_url", "test", DbType.String);
        param.Add("@neighborhood_id", newAdvert.NeighborhoodId, DbType.Int16);
        param.Add("@building_floor", newAdvert.BuildingFloor, DbType.Int16);
        param.Add("@advert_purpose_id", newAdvert.AdvertPurposeId, DbType.Int16);
        param.Add("@advert_type_id", newAdvert.AdvertTypeId, DbType.Int16);
        param.Add("@user_id", userId, DbType.String);

        using (var connection = _dapperContext.CreateConnection())
        {
            var advertId = await connection.QuerySingleAsync<int>(query, param);

            return advertId;
        }
    }

    public async Task<AllInfomrationsAboutAdvertDto> GetAdvert(int advertId)
    {
        var advertMap = new Dictionary<int, AllInfomrationsAboutAdvertDto>();
        var query = AdvertQuery.SingleAdvertWithImages;

        using (var connection = _dapperContext.CreateConnection())
        {
            var adverts = await connection
                .QueryAsync<AllInfomrationsAboutAdvertDto, ShowImageDto, AllInfomrationsAboutAdvertDto>(query,
                map: (advert, image) =>
                {
                    if (advertMap.TryGetValue(advertId, out AllInfomrationsAboutAdvertDto existingAdvert))
                    {
                        advert = existingAdvert;
                    }
                    else
                    {
                        advert.Images = new List<ShowImageDto>
                        {
                            new ShowImageDto {Url = advert.Cover_Image_Url }
                        };
                        advertMap.Add(advertId, advert);
                    }
                    advert.Images.Add(image);
                    return advert;
                }, splitOn: "url",
                param: new { Id = advertId });

            return advertMap.Values.Single();
        }

    }

    public async Task<IEnumerable<ShowAdvertLocationOnMapDto>> GetMapPoints(CancellationToken cancellationToken)
    {
        var query = AdvertQuery.AllAdvertMapPoints;

        var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

        using (var connection = _dapperContext.CreateConnection())
        {
            var mapPoints = await connection.QueryAsync<ShowAdvertLocationOnMapDto>(cmd);

            return mapPoints;
        }


    }

    public async Task<MinimalInformationsAboutAdvertDto> GetAdvertFromMapPoint(int id, CancellationToken cancellationToken)
    {
        var query = AdvertQuery.SingleAdvertForMapPoint;

        var param = new DynamicParameters();

        param.Add("id", id, DbType.Int32);

        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

        using (var connection = _dapperContext.CreateConnection())
        {
            var mapAdvert = await connection.QuerySingleOrDefaultAsync<MinimalInformationsAboutAdvertDto>(cmd);

            return mapAdvert;
        }

    }

    public async Task<Pagination<MinimalInformationsAboutAdvertDto>> GetAdverts(AdvertParameters advertParameters)
    {

        var orderBy = OrderQueryBuilder.CreateOrderQuery<MinimalInformationsAboutAdvertDto>(advertParameters.OrderBy, 'a');

        var rawQuery = AdvertQuery.MakeGetAdvertQuery(advertParameters, orderBy);

        var skip = (advertParameters.PageNumber - 1) * advertParameters.PageSize;

        var param = new DynamicParameters();

        param.Add("skip", skip, DbType.Int32);
        param.Add("take", advertParameters.PageSize, DbType.Int32);
        param.Add("minPrice", advertParameters.MinPrice, DbType.Int32);
        param.Add("maxPrice", advertParameters.MaxPrice, DbType.Int32);
        param.Add("noOfBadrooms", advertParameters.NoOfBadrooms);
        param.Add("noOfBathrooms", advertParameters.NoOfBathrooms);
        param.Add("advertTypeIds", advertParameters.AdvertTypeIds);
        param.Add("advertPurposeIds", advertParameters.AdvertPurposeIds);
        param.Add("cityId", advertParameters.CityId, DbType.Int32);
        param.Add("neighborhoodIds", advertParameters.NeighborhoodIds);
        param.Add("@minFloorSpace", advertParameters.MinFloorSpace, DbType.Int32);
        param.Add("@maxFloorSpace", advertParameters.MaxFloorSpace, DbType.Int32);

        using (var connection = _dapperContext.CreateConnection())
        {
            var multi = await connection.QueryMultipleAsync(rawQuery, param);

            var count = await multi.ReadSingleAsync<int>();
            var adverts = (await multi.ReadAsync<MinimalInformationsAboutAdvertDto>()).ToList();

            var metadata = new PagedList<MinimalInformationsAboutAdvertDto>(adverts, count, advertParameters.PageNumber, advertParameters.PageSize);

            return new Pagination<MinimalInformationsAboutAdvertDto> { Data = adverts, MetaData = metadata.MetaData };
        }
    }

    public async Task UpdateAdvertCoverImage(string imageUrl, int advertId)
    {
        var query = AdvertQuery.UpdateCoverImageQuery;

        var param = new DynamicParameters();

        param.Add("coverImageUrl", imageUrl, DbType.String);
        param.Add("advertId", advertId, DbType.Int32);

        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, param);
        }
    }

    public async Task UpdateStatus(int advertId)
    {
        var query = AdvertQuery.UpdateAdvertStatus;

        var param = new DynamicParameters();

        param.Add("advertId", advertId, DbType.Int32);

        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, param);
        }
    }
}