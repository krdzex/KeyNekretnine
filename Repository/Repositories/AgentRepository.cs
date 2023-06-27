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

    public async Task<int> CreateAgentAndReturnId(CreateAgentDto createAgentDto, CancellationToken cancellationToken)
    {
        var query = AgentQuery.CreateAgentQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("firstName", createAgentDto.LastName, DbType.String);
            param.Add("lastName", createAgentDto.LastName, DbType.String);
            param.Add("phoneNumber", createAgentDto.PhoneNumber, DbType.String);
            param.Add("imageUrl", createAgentDto.ImageUrl, DbType.String);
            param.Add("description", createAgentDto.Description, DbType.String);
            param.Add("email", createAgentDto.Email, DbType.String);
            param.Add("twitterUrl", createAgentDto.TwitterUrl, DbType.String);
            param.Add("facebookUrl", createAgentDto.FacebookUrl, DbType.String);
            param.Add("instagramUrl", createAgentDto.InstagramUrl, DbType.String);
            param.Add("linkedinUrl", createAgentDto.LinkedinUrl, DbType.String);
            param.Add("agencyId", createAgentDto.AgencyId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var agentId = await connection.QuerySingleAsync<int>(cmd);

            return agentId;
        }
    }

    public async Task<Pagination<MinimalAgentInformationsDto>> GetAgents(AgentParameters agentParameters, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<MinimalAgentInformationsDto>(agentParameters.OrderBy, 'a');

        var query = AgentQuery.MakeGetAgentsQuery(orderBy);

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
        var query = AgentQuery.GetAgentAdvertsQuery;

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
        var query = AgentQuery.GetAgentByIdQuery;

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

    public async Task AddLanguageToAgent(int languageId, int agentId, CancellationToken cancellationToken)
    {
        var query = AgentQuery.AssignLanguageToAgentQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("languageId", languageId, DbType.Int16);
            param.Add("agentId", agentId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task DeleteLanguagesForAgent(int agentId, CancellationToken cancellationToken)
    {
        var query = AgentQuery.DeleteLanguagesForAgentQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agentId", agentId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<string> GetAgentImage(int agentId, CancellationToken cancellationToken)
    {
        var query = AgentQuery.GetAgentImageQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agentId", agentId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var imageUrl = await connection.QueryFirstOrDefaultAsync<string>(cmd);

            return imageUrl;
        }
    }

    public async Task UpdateAgent(UpdateAgentDto updateAgentDto, int agentId, CancellationToken cancellationToken)
    {
        var query = AgentQuery.UpdateAgentQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("firstName", updateAgentDto.LastName, DbType.String);
            param.Add("lastName", updateAgentDto.LastName, DbType.String);
            param.Add("phoneNumber", updateAgentDto.PhoneNumber, DbType.String);
            param.Add("imageUrl", updateAgentDto.ImageUrl, DbType.String);
            param.Add("description", updateAgentDto.Description, DbType.String);
            param.Add("email", updateAgentDto.Email, DbType.String);
            param.Add("twitterUrl", updateAgentDto.TwitterUrl, DbType.String);
            param.Add("facebookUrl", updateAgentDto.FacebookUrl, DbType.String);
            param.Add("instagramUrl", updateAgentDto.InstagramUrl, DbType.String);
            param.Add("linkedinUrl", updateAgentDto.LinkedinUrl, DbType.String);
            param.Add("agentId", agentId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<bool> DoesAgentExist(int agentId, CancellationToken cancellationToken)
    {
        var query = AgentQuery.DoesAgentExistQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agentId", agentId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var result = await connection.QueryFirstOrDefaultAsync<bool>(cmd);

            return result;
        }
    }

    public async Task<int> AgencyIdOfAgent(int agentId, CancellationToken cancellationToken)
    {
        var query = AgentQuery.GetAgencyIdFromUserQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agentId", agentId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var result = await connection.QueryFirstOrDefaultAsync<int>(cmd);

            return result;
        }
    }
}