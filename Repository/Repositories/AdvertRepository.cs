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

    public async Task<AllInfomrationsAboutAdvertDto> GetAdvert(int advertId)
    {
        var advertMap = new Dictionary<int, AllInfomrationsAboutAdvertDto>();
        var singleAdvertQuery = AdvertQuery.SingleAdvertWithImages;

        using (var connection = _dapperContext.CreateConnection())
        {
            var adverts = await connection
                .QueryAsync<AllInfomrationsAboutAdvertDto, ShowImageDto, AllInfomrationsAboutAdvertDto>(singleAdvertQuery,
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
        var allAdvertMapPointsQuery = AdvertQuery.AllAdvertMapPoints;

        var cmd = new CommandDefinition(allAdvertMapPointsQuery, cancellationToken: cancellationToken);

        using (var connection = _dapperContext.CreateConnection())
        {
            var mapPoints = await connection.QueryAsync<ShowAdvertLocationOnMapDto>(cmd);

            return mapPoints;
        }


    }

    public async Task<MinimalInformationsAboutAdvertDto> GetAdvertFromMapPoint(int id, CancellationToken cancellationToken)
    {
        var singleAdvertForMapPointQuery = AdvertQuery.SingleAdvertForMapPoint;

        var cmd = new CommandDefinition(singleAdvertForMapPointQuery, new { id }, cancellationToken: cancellationToken);

        using (var connection = _dapperContext.CreateConnection())
        {
            var mapAdvert = await connection.QuerySingleOrDefaultAsync<MinimalInformationsAboutAdvertDto>(cmd);

            return mapAdvert;
        }

    }

    public async Task<Pagination<MinimalInformationsAboutAdvertDto>> GetAdverts(AdvertParameters advertParameters)
    {
        if (!advertParameters.ValidPriceRange)
        {
            throw new ArgumentException("Bad price range");
        }

        var orderBy = OrderQueryBuilder.CreateOrderQuery<MinimalInformationsAboutAdvertDto>(advertParameters.OrderBy, 'a');

        var rawQuery = AdvertQuery.MakeGetAdvertQuery(
            advertParameters.NoOfBadrooms,
            advertParameters.NoOfBathrooms,
            advertParameters.AdvertTypeIds,
            advertParameters.AdvertPurposeIds,
            orderBy, advertParameters.CityId,
            advertParameters.NeighborhoodIds
            );

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

        using (var connection = _dapperContext.CreateConnection())
        {
            var multi = await connection.QueryMultipleAsync(rawQuery, param);

            var count = await multi.ReadSingleAsync<int>();
            var adverts = (await multi.ReadAsync<MinimalInformationsAboutAdvertDto>()).ToList();

            var metadata = new PagedList<MinimalInformationsAboutAdvertDto>(adverts, count, advertParameters.PageNumber, advertParameters.PageSize);

            return new Pagination<MinimalInformationsAboutAdvertDto> { Data = adverts, MetaData = metadata.MetaData };
        }
    }
}