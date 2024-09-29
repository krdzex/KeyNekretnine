using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetLocationUpdate;
internal sealed class LocationUpdateHandler : IQueryHandler<GetLocationUpdateQuery, LocationUpdateResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public LocationUpdateHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }


    public async Task<Result<LocationUpdateResponse>> Handle(GetLocationUpdateQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var updateType = (int)UpdateTypes.Location;

        const string sql = """
            SELECT
                au.id,
                au.approved_on_date AS approvedOnDate,
                au.rejected_on_date AS rejectedOnDate,
                a.location_address AS address,
                a.location_latitude AS latitude,
                a.location_longitude AS longitude,
                a.neighborhood_id AS neighborhoodId,
                au.content
            FROM advert_updates AS au
            INNER JOIN adverts AS a ON a.id = au.advert_id
            WHERE au.id = @UpdateId and au.type = @updateType
            """;

        var updateResult = await connection.QueryAsync<LocationUpdateResponse, LocationAdvertInformations, string, LocationUpdateResponse>(
        sql,
        (update, currentValues, newValue) =>
            {
                var newValues = JsonConvert.DeserializeObject<LocationAdvertInformations>(newValue)!;

                update.AddChange("address", currentValues.Address, newValues.Address);
                update.AddChange("latitude", currentValues.Latitude, newValues.Latitude);
                update.AddChange("longitude", currentValues.Longitude, newValues.Longitude);
                update.AddChange("neighborhoodId", currentValues.NeighborhoodId, newValues.NeighborhoodId);

                return update;

            }, new { request.UpdateId, updateType }, splitOn: "address,content");

        var locationUpdate = updateResult.FirstOrDefault();

        if (locationUpdate is null)
        {
            return Result.Failure<LocationUpdateResponse>(AdvertErrors.LocationUpdateNotFound);
        }

        return locationUpdate;
    }
}