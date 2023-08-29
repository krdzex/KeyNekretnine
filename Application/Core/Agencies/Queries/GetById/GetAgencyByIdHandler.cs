using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Agencies.Queries.GetById;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyById;
internal sealed class GetAgencyByIdHandler : IQueryHandler<GetAgencyByIdQuery, AgencyResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgencyByIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AgencyResponse>> Handle(GetAgencyByIdQuery request, CancellationToken cancellationToken)
    {

        using var connection = _sqlConnectionFactory.CreateConnection();

        Dictionary<int, AgencyResponse> agenciesDictionary = new();

        const string sql = """
            SELECT 
                a.id,
                a.name,
                a.email,
                a.facebook_url AS facebookUrl,
                a.instagram_url AS instagramUrl,
                a.linkedin_url AS linkedinUrl,
                a.description,
                a.address,
                a.twitter_url AS twitterUrl,
                a.website_url AS websiteUrl,
                a.work_start_time AS workStartTime,
                a.work_end_time AS workEndTime,
                a.email,
                a.image_url AS ImageUrl,
                l.id AS LanguageId,
                l.name
            FROM agencies a
            LEFT JOIN agency_languages al ON al.agency_id = a.id
            LEFT JOIN languages l ON al.language_id = l.id
            WHERE a.id = @agencyId;
            """;

        var cmd = new CommandDefinition(sql, new { request.AgencyId }, cancellationToken: cancellationToken);

        var agency = await connection.QueryAsync<AgencyResponse, LanguageResponse, AgencyResponse>(sql, (agency, language) =>
        {
            if (agenciesDictionary.TryGetValue(agency.Id, out var existingAgency))
            {
                agency = existingAgency;
            }
            else
            {
                agenciesDictionary.Add(agency.Id, agency);
            }
            agency.Languages.Add(language);
            return agency;

        }, new { request.AgencyId }, splitOn: "LanguageId");

        if (agenciesDictionary.Count <= 0)
        {
            return Result.Failure<AgencyResponse>(AgencyErrors.AgencyNotFound);
        }

        var agencyResponse = agenciesDictionary[request.AgencyId];

        if (agencyResponse is null)
        {
            return Result.Failure<AgencyResponse>(AgencyErrors.AgencyNotFound);
        }

        return agencyResponse;
    }
}