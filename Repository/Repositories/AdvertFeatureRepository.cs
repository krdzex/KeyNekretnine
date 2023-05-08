using Contracts;
using Dapper;
using Repository.RawQuery;
using System.Data;

namespace Repository.Repositories;
internal sealed class AdvertFeatureRepository : IAdvertFeatureRepository
{
    private readonly DapperContext _dapperContext;
    public AdvertFeatureRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task InsertFeature(string featureName, int advertId, CancellationToken cancellationToken)
    {
        var query = AdvertFeatureQuery.InsertFeatureQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("name", featureName, DbType.String);
            param.Add("advert_id", advertId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }
};