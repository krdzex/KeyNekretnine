using Shared.RequestFeatures;
using System.Text;

namespace Repository.RawQuery;
public static class AdvertQuery
{
    public const string SingleAdvertQuery =
      @"SELECT a.Id,a.price,a.description_sr,a.description_en,a.floor_space,a.street,a.no_of_bedrooms,a.no_of_bathrooms,a.building_floor,a.has_elevator,a.has_garage,a.has_terrace,a.latitude,a.longitude,a.has_wifi,a.is_furnished,a.created_date,a.year_of_building_created,a.cover_image_url,n.name as neighborhood_name,c.name as city_name, c.id as city_id,p.name_sr AS purpose_name_sr,p.name_en AS purpose_name_en,t.name_sr AS type_name_sr,t.name_en AS type_name_en,CONCAT(u.first_name,' ', u.last_name) AS creator, i.url
            FROM adverts a
            INNER JOIN images i ON i.advert_id = a.id
            INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
            INNER JOIN cities c on n.city_id = c.id
            INNER JOIN advert_purposes p ON a.advert_purpose_id = p.id
            INNER JOIN advert_types t ON a.advert_type_id = t.id
            INNER JOIN ""AspNetUsers"" u ON a.user_id = u.id
            WHERE a.id = @id
            AND a.advert_status_id = 1";

    public const string SingleAdminAdvertQuery =
        @"SELECT a.Id,a.price,a.description_sr,a.description_en,a.floor_space,a.street,a.no_of_bedrooms,a.no_of_bathrooms,a.building_floor,a.has_elevator,a.has_garage,a.has_terrace,a.latitude,a.longitude,a.has_wifi,a.is_furnished,a.created_date,a.year_of_building_created,a.cover_image_url,n.name as neighborhood_name,c.name as city_name, c.id as city_id,p.name_sr AS purpose_name_sr,p.name_en AS purpose_name_en,t.name_sr AS type_name_sr,t.name_en AS type_name_en,s.name_sr AS status_name_sr,s.name_en AS status_name_en,CONCAT(u.first_name,' ', u.last_name) AS creator, i.url
            FROM adverts a
            INNER JOIN images i ON i.advert_id = a.id
            INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
            INNER JOIN cities c on n.city_id = c.id
            INNER JOIN advert_purposes p ON a.advert_purpose_id = p.id
            INNER JOIN advert_types t ON a.advert_type_id = t.id
            INNER JOIN ""AspNetUsers"" u ON a.user_id = u.id
            INNER JOIN advert_statuses s ON a.advert_status_id = s.id 
            WHERE a.id = @id
            AND a.advert_status_id != 4";

    public static string MakeGetAdvertQuery(AdvertParameters advertParameters, string orderBy)
    {
        var countConditions = new StringBuilder
            (
            "\t     WHERE a.price >= @minPrice AND a.price <= @maxPrice AND a.advert_status_id = 1 AND a.floor_space >= @minFloorSpace  AND a.floor_space <= @maxFloorSpace "
            );

        var countAdvertQuery = new StringBuilder
            (
            @"SELECT COUNT(a.id)
             FROM adverts a"
            );

        var selectAdvertsQuery = new StringBuilder
            (
            @"SELECT a.id,a.price,a.floor_space,a.no_of_bedrooms,a.no_of_bathrooms,a.created_date,a.cover_image_url,CONCAT(c.name, ', ', n.name) AS location,p.name_en AS purpose_name_en,p.name_sr AS purpose_name_sr,a.street
             FROM adverts a
             INNER JOIN advert_purposes p ON a.advert_purpose_id = p.id
             INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
             INNER JOIN cities c ON n.city_id = c.id"
            );

        if (advertParameters.NoOfBedrooms is not null) countConditions.AppendLine(" AND (a.no_of_bedrooms = ANY(@noOfBedrooms) OR ((select get_max_value(@noOfBedrooms)) >= 4 AND a.no_of_bedrooms >= 4))");
        if (advertParameters.NoOfBathrooms is not null) countConditions.AppendLine(" AND (a.no_of_bathrooms = ANY(@noOfBathrooms) OR ((select get_max_value(@noOfBathrooms)) >= 4 AND a.no_of_bathrooms >= 4))");
        if (advertParameters.AdvertTypeIds is not null) countConditions.AppendLine(" AND a.advert_type_id = ANY(@advertTypeIds)");
        if (advertParameters.AdvertPurposeIds is not null) countConditions.AppendLine(" AND a.advert_purpose_id = ANY(@advertPurposeIds)");

        if (advertParameters.CityId is not null)
        {
            countAdvertQuery.AppendLine(" INNER JOIN neighborhoods n ON a.neighborhood_id = n.id\r\t     INNER JOIN cities c ON n.city_id = c.id");
            countConditions.AppendLine(" AND c.id = @cityId");
        }
        if (advertParameters.CityId is null && advertParameters.NeighborhoodIds is not null)
        {
            countAdvertQuery.AppendLine(" INNER JOIN neighborhoods n ON a.neighborhood_id = n.id\n");
            countConditions.AppendLine(" AND a.neighborhood_id = ANY(@neighborhoodIds)");
        }
        countAdvertQuery.Append(countConditions).Append(';');
        selectAdvertsQuery.Append(countConditions).Append($" ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;");

        return countAdvertQuery.ToString() + selectAdvertsQuery.ToString();
    }

    public const string AllAdvertMapPoints =
        @"SELECT a.id,a.latitude,a.longitude
              FROM adverts a
              WHERE a.advert_status_id = 1";

    public const string SingleAdvertForMapPoint =
       @"SELECT a.id,a.price,a.floor_space,a.no_of_bedrooms,a.no_of_bathrooms,a.created_date,a.cover_image_url,CONCAT(c.name, ', ', n.name) AS location,p.name_en AS purpose_name_en,p.name_sr AS purpose_name_sr,a.street
             FROM adverts a
             INNER JOIN advert_purposes p ON a.advert_purpose_id = p.id
             INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
             INNER JOIN cities c ON n.city_id = c.id
             WHERE a.id = @Id
             AND a.advert_status_id = 1";


    public const string AddAdvertQuery =
       @"INSERT INTO adverts (price,description_sr,description_en,floor_space,street,no_of_bedrooms,no_of_bathrooms,has_elevator,has_garage,has_terrace,latitude,longitude,has_wifi,is_furnished,created_date,year_of_building_created,cover_image_url,neighborhood_id,building_floor,advert_purpose_id,advert_status_id,advert_type_id,user_id,reference_id)
            VALUES (@price,@description_sr,@description_en,@floor_space,@street,@no_of_bedrooms,@no_of_bathrooms,@has_elevator,@has_garage,@has_terrace,@latitude,@longitude,@has_wifi,@is_furnished,@created_date,@year_of_building_created,@cover_image_url,@neighborhood_id,@building_floor,@advert_purpose_id,4,@advert_type_id,@user_id,@reference_id)
            RETURNING id";

    public const string UpdateCoverImageQuery =
       @"UPDATE adverts
            SET cover_image_url = @coverImageUrl
            WHERE id = @advertId";

    public const string UpdateAdvertStatus =
       @"UPDATE adverts
            SET advert_status_id = 2
            WHERE id = @advertId";

    public const string AdvertExistQuery =
        @"SELECT COUNT(*)
          FROM adverts
          WHERE id = @AdvertId";

    public const string ApproveAdvertQuery =
        @"UPDATE adverts
          SET advert_status_id = 1
          WHERE id = @AdvertId";

    public const string DeclineAdvertQuery =
        @"UPDATE adverts
          SET advert_status_id = 3
          WHERE id = @AdvertId";

    public static string MakeGetAdminAdvertQuery(IEnumerable<Int32> advertStatusIds, string orderBy)
    {
        var countConditions = new StringBuilder("");
        var countAdvertQuery = new StringBuilder
            (
            @"SELECT COUNT(a.id)
             FROM adverts a"
            );

        var selectAdvertsQuery = new StringBuilder
            (
            @"SELECT a.id,a.price,a.floor_space,a.no_of_bedrooms,a.no_of_bathrooms,a.created_date,a.cover_image_url,CONCAT(c.name, ', ', n.name) AS location,p.name_en AS purpose_name_en,p.name_sr AS purpose_name_sr,a.street,s.name_en AS status_name_en,s.name_sr AS status_name_sr
             FROM adverts a
             INNER JOIN advert_purposes p ON a.advert_purpose_id = p.id
             INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
             INNER JOIN cities c ON n.city_id = c.id
             INNER JOIN advert_statuses s ON a.advert_status_id = s.id "
            );

        if (advertStatusIds is not null) countConditions.AppendLine(" WHERE a.advert_status_id = ANY(@advertStatusIds)");

        countAdvertQuery.Append(countConditions).Append(';');
        selectAdvertsQuery.Append(countConditions).Append($" ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;");

        return countAdvertQuery.ToString() + selectAdvertsQuery.ToString();
    }
}