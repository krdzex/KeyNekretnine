using Dapper;
using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetIsAdvertFavorite;
internal sealed class GetIsAdvertFavoriteHandler : IQueryHandler<GetIsAdvertFavoriteQuery, bool>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;

    public GetIsAdvertFavoriteHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    public async Task<Result<bool>> Handle(GetIsAdvertFavoriteQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                COUNT(f.user_id) > 0 AS isFavorite
            FROM adverts AS a
            LEFT JOIN user_advert_favorites f ON a.id = f.advert_id
            WHERE a.reference_id = @ReferenceId
            AND f.user_id = @UserId;
            """;

        var cmd = new CommandDefinition(sql, new { request.ReferenceId, _userContext.UserId }, cancellationToken: cancellationToken);

        var isFavorite = await connection.QueryFirstAsync<bool>(cmd);

        return isFavorite;
    }
}