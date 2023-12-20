using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAllAdvertCoordinates;
internal sealed class GetAllAdvertCoordinatesHandler : IQueryHandler<GetAllAdvertCoordinatesQuery, IReadOnlyList<AdvertCoordinateResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllAdvertCoordinatesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<AdvertCoordinateResponse>>> Handle(GetAllAdvertCoordinatesQuery request, CancellationToken cancellationToken)
    {
        var advertAccepted = (int)AdvertStatus.Accepted;

        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = $"""
            SELECT
                reference_id AS referenceId,
                location_latitude AS latitude,
                location_longitude AS longitude
            FROM adverts 
            WHERE status = {advertAccepted}
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var coordinates = await connection.QueryAsync<AdvertCoordinateResponse>(cmd);

        return coordinates.ToList();
    }
}