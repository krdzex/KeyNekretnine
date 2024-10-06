using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetFeaturesUpdate;
internal sealed class GetFeaturesUpdateHandler : IQueryHandler<GetFeaturesUpdateQuery, FeaturesUpdateResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetFeaturesUpdateHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }


    public async Task<Result<FeaturesUpdateResponse>> Handle(GetFeaturesUpdateQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var updateType = (int)UpdateTypes.Features;

        const string sql = """
            SELECT
                au.id,
                au.approved_on_date AS approvedOnDate,
                au.rejected_on_date AS rejectedOnDate,
                au.old_content as oldContent,
                au.new_content as newContent
            FROM advert_updates AS au
            WHERE au.id = @UpdateId AND au.type = @updateType
            """;

        var currentValuesArray = new List<string>();
        var newValues = new List<string>();

        var updateResult = await connection.QueryAsync<FeaturesUpdateResponse, string, string, FeaturesUpdateResponse>(
            sql,
        (update, oldValues, newValues) =>
        {
            var oldValuesObj = JsonConvert.DeserializeObject<FeaturesInformations>(oldValues)!;
            var newValuesObj = JsonConvert.DeserializeObject<FeaturesInformations>(newValues)!;

            var oldValuesString = string.Join(", ", oldValuesObj.Features);
            var newValuesString = string.Join(", ", newValuesObj.Features);

            update.AddChange("features", oldValuesString, newValuesString);

            return update;
        }, new { request.UpdateId, updateType }, splitOn: "oldContent,newContent");

        var featuresUpdate = updateResult.FirstOrDefault();

        if (featuresUpdate is null)
        {
            return Result.Failure<FeaturesUpdateResponse>(AdvertErrors.FeaturesUpdateNotFound);
        }

        return featuresUpdate;
    }
}