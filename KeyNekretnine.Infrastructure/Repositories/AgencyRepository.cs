using KeyNekretnine.Domain.Agencies;
using Microsoft.EntityFrameworkCore;

namespace KeyNekretnine.Infrastructure.Repositories;
internal sealed class AgencyRepository : Repository<Agency>, IAgencyRepository
{
    public AgencyRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Agency?> GetByIdWithLanguagesAsync(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Agency>()
            .Include(agency => agency.AgencyLanguages)
            .FirstOrDefaultAsync(agency => agency.Id == id, cancellationToken);
    }
    //public async Task CreateAgency(string name, string userId, CancellationToken cancellationToken)
    //{
    //    var query = AgencyQuery.CreateAgencyQuery;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();

    //        param.Add("name", name, DbType.String);
    //        param.Add("createdDate", DateTime.Now);
    //        param.Add("userId", userId, DbType.String);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        await connection.ExecuteAsync(cmd);
    //    }
    //}

    //public async Task<bool> DoesAgencyExist(int agencyId, CancellationToken cancellationToken)
    //{
    //    var query = AgencyQuery.DoesAgencyExistQuery;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();

    //        param.Add("agencyId", agencyId, DbType.Int32);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        var result = await connection.QueryFirstOrDefaultAsync<bool>(cmd);

    //        return result;
    //    }
    //}

    //public async Task<bool> IsUserAgencyOwner(string userId, int agencyId, CancellationToken cancellationToken)
    //{
    //    var query = AgencyQuery.IsUserAgencyOwnerQuery;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();

    //        param.Add("agencyId", agencyId, DbType.Int32);
    //        param.Add("userId", userId, DbType.String);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        var result = await connection.QueryFirstOrDefaultAsync<bool>(cmd);

    //        return result;
    //    }
    //}

    //public async Task<GetAgencyDto> GetAgencyById(int agencyId, CancellationToken cancellationToken)
    //{
    //    var query = AgencyQuery.GetAgencyByIdQuery;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();

    //        param.Add("agencyId", agencyId, DbType.Int32);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        var multi = await connection.QueryMultipleAsync(cmd);

    //        var agency = await multi.ReadSingleOrDefaultAsync<GetAgencyDto>();

    //        if (agency != null)
    //        {
    //            agency.Languages = await multi.ReadAsync<LanguageDto>();
    //        }

    //        return agency;
    //    }
    //}

    //public async Task<string> GetAgencyImage(int agencyId, CancellationToken cancellationToken)
    //{
    //    var query = AgencyQuery.GetAgencyImageQuery;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();

    //        param.Add("agencyId", agencyId, DbType.Int32);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        var imageUrl = await connection.QueryFirstOrDefaultAsync<string>(cmd);

    //        return imageUrl;
    //    }
    //}

    //public async Task UpdateAgency(UpdateAgencyDto updateAgencyDto, int agencyId, CancellationToken cancellationToken)
    //{
    //    var query = AgencyQuery.UpdateAgencyQuery;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();

    //        param.Add("agencyId", agencyId, DbType.Int32);
    //        param.Add("name", updateAgencyDto.Name, DbType.String);
    //        param.Add("location", updateAgencyDto.Address, DbType.String);
    //        param.Add("description", updateAgencyDto.Description, DbType.String);
    //        param.Add("email", updateAgencyDto.Email, DbType.String);
    //        param.Add("websiteUrl", updateAgencyDto.WebsiteUrl, DbType.String);
    //        param.Add("workStartTime", updateAgencyDto.WorkStartTime is not null ? TimeSpan.ParseExact(updateAgencyDto.WorkStartTime, @"hh\:mm", CultureInfo.CurrentCulture) : null);
    //        param.Add("workEndTime", updateAgencyDto.WorkEndTime is not null ? TimeSpan.ParseExact(updateAgencyDto.WorkEndTime, @"hh\:mm", CultureInfo.CurrentCulture) : null);
    //        param.Add("twitterUrl", updateAgencyDto.TwitterUrl, DbType.String);
    //        param.Add("facebookUrl", updateAgencyDto.FacebookUrl, DbType.String);
    //        param.Add("instagramUrl", updateAgencyDto.InstagramUrl, DbType.String);
    //        param.Add("linkedinUrl", updateAgencyDto.LinkedinUrl, DbType.String);
    //        param.Add("latitude", updateAgencyDto.Latitude, DbType.Double);
    //        param.Add("longitude", updateAgencyDto.Longitude, DbType.Double);
    //        param.Add("imageUrl", updateAgencyDto.Image, DbType.String);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        await connection.ExecuteAsync(cmd);
    //    }
    //}

    //public async Task AddLanguageToAgency(int languageId, int agencyId, CancellationToken cancellationToken)
    //{
    //    var query = AgencyQuery.AssignLanguageToAgencyQuery;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();

    //        param.Add("languageId", languageId, DbType.Int16);
    //        param.Add("agencyId", agencyId, DbType.Int16);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        await connection.ExecuteAsync(cmd);
    //    }
    //}

    //public async Task DeleteLanguagesForAgency(int agencyId, CancellationToken cancellationToken)
    //{
    //    var query = AgencyQuery.DeleteLanguagesForAgencyQuery;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();

    //        param.Add("agencyId", agencyId, DbType.Int16);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        await connection.ExecuteAsync(cmd);
    //    }
    //}
}