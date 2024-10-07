using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
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
        var updateType = (int)UpdateTypes.BasicInformation;

        const string sql = """
        SELECT
            au.id,
            au.approved_on_date AS approvedOnDate,
            au.old_content as oldContent,
            au.new_content as newContent
        FROM advert_updates AS au
        WHERE au.id = @UpdateId and au.type = @updateType
        """;

        var updateResult = await connection.QueryAsync<BasicUpdateResponse, string, string, BasicUpdateResponse>(
            sql,
            (update, oldValues, newValues) =>
            {
                var oldValuesObj = JsonConvert.DeserializeObject<BasicAdvertInformations>(oldValues)!;
                var newValuesObj = JsonConvert.DeserializeObject<BasicAdvertInformations>(newValues)!;

                update.AddChange("price", oldValuesObj.Price, newValuesObj.Price);
                update.AddChange("floorSpace", oldValuesObj.FloorSpace, newValuesObj.FloorSpace);
                update.AddChange("noOfBedrooms", oldValuesObj.NoOfBedrooms, newValuesObj.NoOfBedrooms);
                update.AddChange("noOfBathrooms", oldValuesObj.NoOfBathrooms, newValuesObj.NoOfBathrooms);
                update.AddChange("buildingFloor", oldValuesObj.BuildingFloor, newValuesObj.BuildingFloor);
                update.AddChange("hasElevator", oldValuesObj.HasElevator, newValuesObj.HasElevator);
                update.AddChange("hasGarage", oldValuesObj.HasGarage, newValuesObj.HasGarage);
                update.AddChange("hasTerrace", oldValuesObj.HasTerrace, newValuesObj.HasTerrace);
                update.AddChange("hasWifi", oldValuesObj.HasWifi, newValuesObj.HasWifi);
                update.AddChange("isFurnished", oldValuesObj.IsFurnished, newValuesObj.IsFurnished);
                update.AddChange("yearOfBuildingCreated", oldValuesObj.YearOfBuildingCreated, newValuesObj.YearOfBuildingCreated);
                update.AddChange("isUnderConstruction", oldValuesObj.IsUnderConstruction, newValuesObj.IsUnderConstruction);
                update.AddChange("type", oldValuesObj.Type, newValuesObj.Type);
                update.AddChange("purpose", oldValuesObj.Purpose, newValuesObj.Purpose);
                update.AddChange("isUrgent", oldValuesObj.IsUrgent, newValuesObj.IsUrgent);
                update.AddChange("descriptionEn", oldValuesObj.DescriptionEn, newValuesObj.DescriptionEn);
                update.AddChange("descriptionSr", oldValuesObj.DescriptionSr, newValuesObj.DescriptionSr);

                return update;
            },
            new { request.UpdateId, updateType }, splitOn: "oldContent,newContent");

        var basicUpdate = updateResult.FirstOrDefault();

        if (basicUpdate is null)
        {
            return Result.Failure<BasicUpdateResponse>(AdvertErrors.BasicUpdateNotFound);
        }

        return Result.Success(basicUpdate);
    }

}