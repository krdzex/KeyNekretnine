using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.DataTransferObjects.Agency;
using System.Data;

namespace Repository.Repositories;
public class AgentRepository : IAgentRepository
{
    private readonly DapperContext _dapperContext;
    public AgentRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task CreateAgent(CreateAgentDto createAgentDto, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.CreateAgentQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("firstName", createAgentDto.LastName, DbType.String);
            param.Add("lastName", createAgentDto.LastName, DbType.String);
            param.Add("phoneNumber", createAgentDto.PhoneNumber, DbType.String);
            param.Add("imageUrl", createAgentDto.ImageUrl, DbType.String);
            param.Add("agencyId", createAgentDto.AgencyId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }
}