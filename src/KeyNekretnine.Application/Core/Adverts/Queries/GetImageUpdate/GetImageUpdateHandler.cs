using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetImageUpdate;
internal sealed class GetImageUpdateHandler : IQueryHandler<GetImageUpdateQuery, ImagesUpdateResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetImageUpdateHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }


    public async Task<Result<ImagesUpdateResponse>> Handle(GetImageUpdateQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var updateType = (int)UpdateTypes.Image;

        const string sql = """
            SELECT
                au.id,
                au.approved_on_date AS approvedOnDate,
                au.rejected_on_date AS rejectedOnDate,
                au.old_content as oldContent,
                au.new_content as newContent
            FROM advert_updates AS au
            WHERE au.id = @UpdateId AND au.type = @updateType AND au.new_content IS NOT NULL
            """;

        var updateResult = await connection.QueryAsync<ImagesUpdateResponse, string, string, ImagesUpdateResponse>(
            sql,
            (update, oldValues, newValues) =>
            {
                var newValuesObj = JsonConvert.DeserializeObject<ImagesInformations>(newValues)!;

                var newValuesString = string.Join(", ", newValuesObj.Images);

                update.AddChange("images", "", newValuesString);

                return update;
            }, new { request.UpdateId, updateType }, splitOn: "oldContent,newContent");

        var featuresUpdate = updateResult.FirstOrDefault();

        if (featuresUpdate is null)
        {
            return Result.Failure<ImagesUpdateResponse>(AdvertErrors.FeaturesUpdateNotFound);
        }

        return featuresUpdate;
    }
}