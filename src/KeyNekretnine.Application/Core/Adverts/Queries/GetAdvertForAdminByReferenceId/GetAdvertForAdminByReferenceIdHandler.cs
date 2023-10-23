using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertForAdminByReferenceId;
internal sealed class GetAdvertForAdminByReferenceIdHandler : IQueryHandler<GetAdvertForAdminByReferenceIdQuery, AdvertForAdminResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAdvertForAdminByReferenceIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AdvertForAdminResponse>> Handle(GetAdvertForAdminByReferenceIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        Dictionary<string, AdvertForAdminResponse> advertDictionary = new();

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
                a.status,
                a.description_sr AS sr,
            	a.description_en AS en,
                a.location_address AS address,
            	a.location_latitude AS latitude,
            	a.location_longitude AS longitude,
            	n.id AS neighborhoodId,
            	n.name AS neighborhoodName,
            	c.id AS cityId,
            	c.name as cityName,
                u.first_name AS firstName,
                u.last_name AS lastName,
                u.profile_image_url AS profileImageUrl,
                i.url,
                f.id,
                f.name
            FROM adverts AS a
            INNER JOIN neighborhoods AS n ON a.neighborhood_id = n.id
            INNER JOIN cities AS c ON n.city_id = c.id
            INNER JOIN asp_net_users AS u ON u.id = a.user_id
            LEFT JOIN images AS i ON i.advert_id = a.id
            LEFT JOIN advert_features AS f ON f.advert_id = a.id
            WHERE a.reference_id = @ReferenceId
            AND a.status != 4;
            """;

        var advert = await connection.QueryAsync<AdvertForAdminResponse, AdvertDescriptionResponse, AdvertLocationResponse, AdvertCreatorResponse, AdvertImageResponse, AdvertFeatureResponse, AdvertForAdminResponse>(
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

                if (image is not null)
                {
                    advert.Images.Add(image);
                }

                if (feature is not null)
                {
                    advert.Features.Add(feature);
                }

                return advert;

            }, new { request.ReferenceId }, splitOn: "sr,address,firstName,url,id");

        var advertResponse = advertDictionary.Values.FirstOrDefault();

        if (advertResponse is null)
        {
            return Result.Failure<AdvertForAdminResponse>(AdvertErrors.NotFoundForAdmin);
        }

        return advertResponse;
    }
}