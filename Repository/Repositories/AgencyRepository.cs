using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Agency;
using Shared.DataTransferObjects.Language;
using Shared.RequestFeatures;
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

    public async Task<bool> DoesAgencyExist(int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.DoesAgencyExistQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agencyId", agencyId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var result = await connection.QueryFirstOrDefaultAsync<bool>(cmd);

            return result;
        }
    }

    public async Task<bool> IsUserAgencyOwner(string userId, int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.IsUserAgencyOwnerQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agencyId", agencyId, DbType.Int32);
            param.Add("userId", userId, DbType.String);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var result = await connection.QueryFirstOrDefaultAsync<bool>(cmd);

            return result;
        }
    }

    public async Task CreateImaginaryAgent(ImaginaryAgentDto imaginaryAgent, int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.CreateAgentQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("firstName", imaginaryAgent.LastName, DbType.String);
            param.Add("lastName", imaginaryAgent.LastName, DbType.String);
            param.Add("phoneNumber", imaginaryAgent.PhoneNumber, DbType.String);
            param.Add("imageUrl", imaginaryAgent.ImageUrl, DbType.String);
            param.Add("agencyId", agencyId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<GetAgencyDto> GetAgencyById(int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.GetAgencyByIdQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agencyId", agencyId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);

            var agency = await multi.ReadSingleOrDefaultAsync<GetAgencyDto>();

            if (agency != null)
            {
                agency.Languages = (await multi.ReadAsync<LanguageDto>());
            }

            return agency;
        }
    }

    public async Task<Pagination<GetAgenciesDto>> GetAgencies(AgencyParameters agencyParameters, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<GetAgenciesDto>(agencyParameters.OrderBy, 'a');

        var query = AgencyQuery.MakeGetAgenciesQuery(orderBy);

        var name = !string.IsNullOrEmpty(agencyParameters.Name) ?
            agencyParameters.Name.Trim().ToLower() : string.Empty;

        var skip = (agencyParameters.PageNumber - 1) * agencyParameters.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", agencyParameters.PageSize, DbType.Int32);
        param.Add("name", name);

        using (var connection = _dapperContext.CreateConnection())
        {
            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var multi = await connection.QueryMultipleAsync(cmd);
            var count = await multi.ReadSingleAsync<int>();
            var agencies = (await multi.ReadAsync<GetAgenciesDto>()).ToList();

            var metadata = new PagedList<GetAgenciesDto>(agencies, count, agencyParameters.PageNumber, agencyParameters.PageSize);

            return new Pagination<GetAgenciesDto> { Data = agencies, MetaData = metadata.MetaData };
        }
    }
}
