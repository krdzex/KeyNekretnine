using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetBasicUpdate;
internal sealed class GetBasicUpdateHandler : IQueryHandler<GetBasicUpdateQuery, BasicUpdateResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBasicUpdateHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }


    public async Task<Result<BasicUpdateResponse>> Handle(GetBasicUpdateQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                a.id,
                a.price AS price,
                a.floor_space AS floorSpace,
                a.no_of_bedrooms AS noOfBedrooms,
                a.no_of_bathrooms AS noOfBathrooms,
                a.building_floor AS buildingFloor,
                a.has_elevator AS hasElevator,
                a.has_garage AS hasGarage,
                a.has_terrace AS hasTerrace,
                a.has_wifi AS hasWifi,
                a.is_furnished AS isFurnished,
                a.year_of_building_created AS yearOfBuildingCreated,
                a.is_under_construction as isUnderConstruction,
                a.type,
                a.purpose,
                a.is_urgent AS isUrgent,
                au.content
            FROM advert_updates AS au
            INNER JOIN adverts AS a ON a.id = au.advert_id
            WHERE au.id = @UpdateId
            """;

        var updateResult = await connection.QueryAsync<BasicUpdateResponse, BasicAdvertInformations, string, BasicUpdateResponse>(
            sql,
            (update, test, newValue) =>
            {
                update.CurrentValues = test;
                update.NewValues = JsonConvert.DeserializeObject<BasicAdvertInformations>(newValue)!;
                return update;

            }, new { request.UpdateId }, splitOn: "price,content");

        var singleUpdate = updateResult.FirstOrDefault();

        if (singleUpdate is null)
        {
            return Result.Failure<BasicUpdateResponse>(AdvertErrors.BasicUpdateNotFound);
        }

        return singleUpdate;
    }
}