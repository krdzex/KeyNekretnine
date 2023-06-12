using Contracts;
using Dapper;
using Repository.RawQuery;
using System.Data;

namespace Repository.Repositories;
public class AgencyRepository : IAgencyRepository
{
    private readonly DapperContext _dapperContext;
    public AgencyRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task CreateAgency(string name, string userId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.CreateAgencyQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("name", name, DbType.String);
            param.Add("createdDate", DateTime.Now);
            param.Add("userId", userId, DbType.String);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }
}
