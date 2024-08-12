using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyLocation;
using KeyNekretnine.Application.Core.Language.Queries.GetLanguages;
using KeyNekretnine.Application.Core.Shared;
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

        Dictionary<Guid, AgencyResponse> agenciesDictionary = new();

        const string sql = """
            SELECT 
                a.id,
                a.name,
                a.email,
                a.description,
                a.phone_number AS phoneNumber,
                a.location_address AS address,
                a.website_url AS websiteUrl,
                a.work_hour_start AS workStartTime,
                a.work_hour_end AS workEndTime,
                a.email,
                a.image_url AS image,
                a.location_latitude AS latitude,
                a.location_longitude AS longitude,
                a.location_address AS address,
                a.social_media_facebook AS facebook,
                a.social_media_instagram AS instagram,
                a.social_media_linkedin AS linkedin,
                a.social_media_twitter AS twitter,
                l.name
            FROM agencies a
            LEFT JOIN agency_languages al ON al.agency_id = a.id
            LEFT JOIN languages l ON al.language_id = l.id
            WHERE a.id = @agencyId;
            """;

        var agency = await connection.QueryAsync<AgencyResponse, AgencyLocationResponse, SocialMediaResponse, LanguageResponse, AgencyResponse>(sql, (agency, location, socialMedia, language) =>
        {
            if (agenciesDictionary.TryGetValue(agency.Id, out var existingAgency))
            {
                agency = existingAgency;
            }
            else
            {
                agenciesDictionary.Add(agency.Id, agency);
            }

            agency.SocialMedia = socialMedia;
            agency.Location = location;

            if (language is not null)
            {
                agency.Languages.Add(language);
            }
            return agency;

        }, new { request.AgencyId }, splitOn: "latitude,facebook,name");

        var agencyResponse = agenciesDictionary.Values.FirstOrDefault();

        if (agencyResponse is null)
        {
            return Result.Failure<AgencyResponse>(AgencyErrors.NotFound);
        }

        return agencyResponse;
    }
}