using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.DataTransferObjects.Agency;
using Shared.DataTransferObjects.Language;
using Shared.RequestFeatures;
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
        var query = AgentsQuery.CreateAgentQuery;

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

    public async Task<Pagination<MinimalAgentInformationsDto>> GetAgents(AgentParameters agentParameters, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<MinimalAgentInformationsDto>(agentParameters.OrderBy, 'a');

        var query = AgentsQuery.MakeGetAgentsQuery(orderBy);

        var skip = (agentParameters.PageNumber - 1) * agentParameters.PageSize;

        var param = new DynamicParameters();

        param.Add("skip", skip, DbType.Int32);
        param.Add("take", agentParameters.PageSize, DbType.Int32);

        using (var connection = _dapperContext.CreateConnection())
        {
            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);
            var count = await multi.ReadSingleAsync<int>();
            var agents = (await multi.ReadAsync<MinimalAgentInformationsDto>()).ToList();

            var metadata = new PagedList<MinimalAgentInformationsDto>(agents, count, agentParameters.PageNumber, agentParameters.PageSize);

            return new Pagination<MinimalAgentInformationsDto> { Data = agents, MetaData = metadata.MetaData };
        }
    }

    public async Task<IEnumerable<MinimalInformationsAboutAdvertDto>> GetAgentAdverts(int agentId, CancellationToken cancellationToken)
    {
        var query = AgentsQuery.GetAgentAdvertsQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agentId", agentId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var adverts = await connection.QueryAsync<MinimalInformationsAboutAdvertDto>(cmd);

            return adverts;
        }
    }

    public async Task<AllAgentInformationsDto> GetAgentById(int agentId, CancellationToken cancellationToken)
    {
        var query = AgentsQuery.GetAgentByIdQuery;

        using (var connection = _dapperContext.CreateConnection())
        {

            var param = new DynamicParameters();

            param.Add("agentId", agentId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);

            var agent = await multi.ReadSingleOrDefaultAsync<AllAgentInformationsDto>();

            if (agent != null)
            {
                agent.Languages = (await multi.ReadAsync<LanguageDto>());
            }

            return agent;
        }
    }
}