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
                f.name,
                au.content
            FROM advert_updates AS au
            INNER JOIN adverts AS a ON a.id = au.advert_id
            LEFT JOIN advert_features AS f ON f.advert_id = a.id
            WHERE au.id = @UpdateId AND au.type = @updateType
            """;

        var currentValuesArray = new List<string>();

        var updateResult = await connection.QueryAsync<FeaturesUpdateResponse, string, string, FeaturesUpdateResponse>(
            sql,
            (update, currentValues, newValue) =>
            {
                if (!string.IsNullOrEmpty(currentValues))
                {
                    currentValuesArray.Add(currentValues);
                }

                update.NewValues = JsonConvert.DeserializeObject<FeaturesInformations>(newValue)!;
                return update;

            }, new { request.UpdateId, updateType }, splitOn: "name,content");

        var featuresUpdate = updateResult.FirstOrDefault();

        if (featuresUpdate is null)
        {
            return Result.Failure<FeaturesUpdateResponse>(AdvertErrors.FeaturesUpdateNotFound);
        }

        featuresUpdate.CurrentValues.Features = currentValuesArray;
        return featuresUpdate;
    }
}