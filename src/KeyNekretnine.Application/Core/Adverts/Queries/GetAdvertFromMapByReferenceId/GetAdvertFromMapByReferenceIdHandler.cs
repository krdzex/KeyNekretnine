using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertFromMapByReferenceId;
internal sealed class GetAdvertFromMapByReferenceIdHandler : IQueryHandler<GetAdvertFromMapByReferenceIdQuery, AdvertFromMapResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAdvertFromMapByReferenceIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }


    public async Task<Result<AdvertFromMapResponse>> Handle(GetAdvertFromMapByReferenceIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
            	a.reference_id AS referenceId,
            	a.price,
            	a.floor_space AS floorSpace,
            	a.no_of_bedrooms AS noOfBedrooms,
            	a.no_of_bathrooms AS noOfBathrooms,
            	a.cover_image_url AS coverImageUrl,
            	CONCAT(c.name, ', ', n.name) AS location,
            	a.purpose,
            	a.type,
            	a.location_address AS address,
            	a.is_urgent AS isUrgent
            FROM adverts AS a
            INNER JOIN neighborhoods AS n ON a.neighborhood_id = n.id
            INNER JOIN cities AS c ON n.city_id = c.id
            WHERE a.reference_id = @ReferenceId
            AND a.status = 1
            """;

        var cmd = new CommandDefinition(sql, new { request.ReferenceId }, cancellationToken: cancellationToken);

        var advertFromMap = await connection.QueryFirstOrDefaultAsync<AdvertFromMapResponse>(cmd);

        if (advertFromMap is null)
        {
            return Result.Failure<AdvertFromMapResponse>(AdvertErrors.NotFound);
        }

        return advertFromMap;
    }
}