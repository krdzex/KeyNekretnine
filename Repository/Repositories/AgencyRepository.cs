﻿using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.DataTransferObjects.Agency;
using Shared.DataTransferObjects.Language;
using Shared.RequestFeatures;
using System.Data;
using System.Globalization;

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

    public async Task<string> GetAgencyImage(int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.GetAgencyImageQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agencyId", agencyId, DbType.Int32);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var imageUrl = await connection.QueryFirstOrDefaultAsync<string>(cmd);

            return imageUrl;
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

    public async Task UpdateAgency(UpdateAgencyDto updateAgencyDto, int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.UpdateAgencyQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agencyId", agencyId, DbType.Int32);
            param.Add("name", updateAgencyDto.Name, DbType.String);
            param.Add("location", updateAgencyDto.Address, DbType.String);
            param.Add("description", updateAgencyDto.Description, DbType.String);
            param.Add("email", updateAgencyDto.Email, DbType.String);
            param.Add("websiteUrl", updateAgencyDto.WebsiteUrl, DbType.String);
            param.Add("workStartTime", updateAgencyDto.WorkStartTime is not null ? TimeSpan.ParseExact(updateAgencyDto.WorkStartTime, @"hh\:mm", CultureInfo.CurrentCulture) : null);
            param.Add("workEndTime", updateAgencyDto.WorkEndTime is not null ? TimeSpan.ParseExact(updateAgencyDto.WorkEndTime, @"hh\:mm", CultureInfo.CurrentCulture) : null);
            param.Add("twitterUrl", updateAgencyDto.TwitterUrl, DbType.String);
            param.Add("facebookUrl", updateAgencyDto.FacebookUrl, DbType.String);
            param.Add("instagramUrl", updateAgencyDto.InstagramUrl, DbType.String);
            param.Add("linkedinUrl", updateAgencyDto.LinkedinUrl, DbType.String);
            param.Add("latitude", updateAgencyDto.Latitude, DbType.Double);
            param.Add("longitude", updateAgencyDto.Longitude, DbType.Double);
            param.Add("imageUrl", updateAgencyDto.Image, DbType.String);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task AddLanguageToAgency(int languageId, int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.AssignLanguageToAgencyQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("languageId", languageId, DbType.Int16);
            param.Add("agencyId", agencyId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task DeleteLanguagesForAgency(int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.DeleteLanguagesForAgencyQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agencyId", agencyId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            await connection.ExecuteAsync(cmd);
        }
    }

    public async Task<IEnumerable<MinimalInformationsAboutAdvertDto>> GetAdvertsForAgency(int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.GetAgencyAdvertsQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agencyId", agencyId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var adverts = await connection.QueryAsync<MinimalInformationsAboutAdvertDto>(cmd);

            return adverts;
        }
    }

    public async Task<IEnumerable<AgentForAgencyDto>> GetAgents(int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.GetAgentsQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agencyId", agencyId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var agents = await connection.QueryAsync<AgentForAgencyDto>(cmd);

            return agents;
        }
    }

    public async Task<AgencyLocationDto> GetAgencyLocation(int agencyId, CancellationToken cancellationToken)
    {
        var query = AgencyQuery.GetAgencyLocatinQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var param = new DynamicParameters();

            param.Add("agencyId", agencyId, DbType.Int16);

            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

            var location = await connection.QueryFirstOrDefaultAsync<AgencyLocationDto>(cmd);

            return location;
        }
    }
}