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
                au.old_content as oldContent,
                au.new_content as newContent
            FROM advert_updates AS au
            WHERE au.id = @UpdateId and au.type = @updateType
            """;

        var updateResult = await connection.QueryAsync<LocationUpdateResponse, string, string, LocationUpdateResponse>(
        sql,
        (update, oldValues, newValues) =>
        {
            var oldValuesObj = JsonConvert.DeserializeObject<LocationAdvertInformations>(oldValues)!;
            var newValuesObj = JsonConvert.DeserializeObject<LocationAdvertInformations>(newValues)!;

            update.AddChange("address", oldValuesObj.Address, newValuesObj.Address);
            update.AddChange("latitude", oldValuesObj.Latitude, newValuesObj.Latitude);
            update.AddChange("longitude", oldValuesObj.Longitude, newValuesObj.Longitude);
            update.AddChange("neighborhoodId", oldValuesObj.NeighborhoodId, newValuesObj.NeighborhoodId);

            return update;

        }, new { request.UpdateId, updateType }, splitOn: "oldContent,newContent");

        var locationUpdate = updateResult.FirstOrDefault();

        if (locationUpdate is null)
        {
            return Result.Failure<LocationUpdateResponse>(AdvertErrors.LocationUpdateNotFound);
        }

        return locationUpdate;
    }
}