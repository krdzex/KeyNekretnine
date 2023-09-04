using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Language.Queries.Get;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetById;
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
                a.description,
                a.address,
                a.website_url AS websiteUrl,
                a.work_start_time AS workStartTime,
                a.work_end_time AS workEndTime,
                a.email,
                a.image_url AS ImageUrl,
                a.facebook_url AS facebook,
                a.instagram_url AS instagram,
                a.linkedin_url AS linkedin,
                a.twitter_url AS twitter,
                l.id,
                l.name
            FROM agencies a
            LEFT JOIN agency_languages al ON al.agency_id = a.id
            LEFT JOIN languages l ON al.language_id = l.id
            WHERE a.id = @agencyId;
            """;

        var agency = await connection.QueryAsync<AgencyResponse, SocialMediaResponse, LanguageResponse, AgencyResponse>(sql, (agency, socialNetwork, language) =>
        {
            if (agenciesDictionary.TryGetValue(agency.Id, out var existingAgency))
            {
                agency = existingAgency;
            }
            else
            {
                agenciesDictionary.Add(agency.Id, agency);
            }
            agency.SocialNetwork ??= socialNetwork;

            if (language is not null)
            {
                agency.Languages.Add(language);
            }
            return agency;

        }, new { request.AgencyId }, splitOn: "facebook,id");

        if (agenciesDictionary.Count <= 0)
        {
            return Result.Failure<AgencyResponse>(AgencyErrors.NotFound);
        }

        var agencyResponse = agenciesDictionary[request.AgencyId];

        return agencyResponse;
    }
}