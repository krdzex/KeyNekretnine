using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertByReferenceId;
internal sealed class GetAdvertByReferenceIdHandler : IQueryHandler<GetAdvertByReferenceIdQuery, AdvertResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAdvertByReferenceIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AdvertResponse>> Handle(GetAdvertByReferenceIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        Dictionary<string, AdvertResponse> advertDictionary = new();

        const string sql = """
            SELECT
                a.reference_id AS referenceId,
            	a.price,
            	a.floor_space AS floorSpace,
            	a.no_of_bedrooms AS noOfBedrooms,
            	a.no_of_bathrooms AS noOfBathrooms,
            	a.building_floor AS buildingFloor,
            	a.has_elevator AS hasElevator,
            	a.has_garage AS hasGarage,
            	a.has_terrace AS hasTerrace,
            	a.has_wifi AS hasWifi,
            	a.is_furnished AS isFurnished,
            	a.created_on_date AS createdOnDate,
            	a.year_of_building_created AS yearOfBuildingCreated,
            	a.cover_image_url AS coverImageUrl,
            	a.is_urgent AS isUrgent,
            	a.is_under_construction as isUnderConstruction,
                a.type,
                a.purpose,
                a.description_sr AS sr,
            	a.description_en AS en,
                a.location_address AS address,
            	a.location_latitude AS latitude,
            	a.location_longitude AS longitude,
            	n.id AS neighborhoodId,
            	n.name AS neighborhoodName,
            	c.slug AS citySlug,
            	c.name as cityName,
                CASE 
                    WHEN a.user_id IS NOT NULL THEN u.first_name
                    ELSE ag.first_name 
                END AS firstName,
                CASE 
                    WHEN a.user_id IS NOT NULL THEN u.last_name 
                    ELSE ag.last_name 
                END AS lastName,
                CASE 
                    WHEN a.user_id IS NOT NULL THEN u.profile_image_url 
                    ELSE ag.image_url 
                END AS lastName,
                CASE 
                    WHEN a.user_id IS NOT NULL THEN u.email 
                    ELSE ag.email 
                END AS email,
                CASE 
                    WHEN a.user_id IS NOT NULL THEN u.phone_number 
                    ELSE ag.phone_number 
                END AS phoneNumber,
                u.id as userId,
                ag.id AS agentId,
                agy.name AS agencyName,
                agy.id AS agencyId,
                agy.image_url AS AgencyImageUrl,
                i.url,
                f.id,
                f.name
            FROM adverts AS a
            INNER JOIN neighborhoods AS n ON a.neighborhood_id = n.id
            INNER JOIN cities AS c ON n.city_id = c.id
            LEFT JOIN asp_net_users AS u ON u.id = a.user_id
            LEFT JOIN agents ag ON a.agent_id = ag.id
            LEFT JOIN agencies agy ON ag.agency_id = agy.id
            LEFT JOIN images AS i ON i.advert_id = a.id
            LEFT JOIN advert_features AS f ON f.advert_id = a.id
            WHERE a.reference_id = @ReferenceId
            AND a.status = 1;
            """;

        var advert = await connection.QueryAsync<AdvertResponse, AdvertDescriptionResponse, AdvertLocationResponse, AdvertCreatorResponse, AdvertImageResponse, AdvertFeatureResponse, AdvertResponse>(
            sql,
            (advert, description, location, creator, image, feature) =>
            {
                if (advertDictionary.TryGetValue(advert.ReferenceId, out var existingAdvert))
                {
                    advert = existingAdvert;
                }
                else
                {
                    advertDictionary.Add(advert.ReferenceId, advert);
                }

                advert.Description = description;
                advert.Location = location;
                advert.Creator = creator;

                if (advert.Images.Count is 0)
                {
                    advert.Images.Add(new AdvertImageResponse { Url = advert.CoverImageUrl });
                }

                if (image is not null && !advert.Images.Any(i => i.Url == image.Url))
                {
                    advert.Images.Add(image);
                }

                if (feature is not null && !advert.Features.Any(i => i.Id == feature.Id))
                {
                    advert.Features.Add(feature);
                }

                return advert;

            }, new { request.ReferenceId }, splitOn: "sr,address,firstName,url,id");

        var advertResponse = advertDictionary.Values.FirstOrDefault();

        if (advertResponse is null)
        {
            return Result.Failure<AdvertResponse>(AdvertErrors.NotFound);
        }

        return advertResponse;
    }
}