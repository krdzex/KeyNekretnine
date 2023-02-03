using System.Text;

namespace Repository.RawQuery;
public static class AdvertQuery
{
    public const string SingleAdvertWithImages =
      @"SELECT a.Id,a.price,a.description,a.floor_space,a.street,a.no_of_badrooms,no_of_bathrooms,a.has_elevator,a.has_garage,a.has_terrace,a.latitude,a.longitude,a.has_wifi,a.is_furnished,a.created_date,a.year_of_building_created,a.cover_image_url,n.name as neighborhood_name,c.name as city_name, c.id as city_id,p.name AS purpose_name,t.name AS type_name,CONCAT(u.first_name,' ', u.last_name) AS creator, i.url
            FROM adverts a
            INNER JOIN images i ON i.advert_id = a.id
            INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
            INNER JOIN cities c on n.city_id = c.id
            INNER JOIN advert_purposes p ON a.advert_purpose_id = p.id
            INNER JOIN advert_types t ON a.advert_type_id = t.id
            INNER JOIN ""AspNetUsers"" u ON a.user_id = u.id
            WHERE a.id = @id";

    public static string MakeGetAdvertQuery(IEnumerable<Int32> noOfBadrooms, IEnumerable<Int32> noOfBathrooms, IEnumerable<Int32> advertTypeIds, IEnumerable<Int32> advertPurposeIds, string orderBy, int? cityId, IEnumerable<Int32> neighborhoodIds)
    {
        var countConditions = new StringBuilder("\t     WHERE a.price >= @minPrice AND a.price <= @maxPrice");
        var countAdvertQuery = new StringBuilder(
            @"SELECT COUNT(a.id)
             FROM adverts a
            "
            );

        var selectAdvertsQuery = new StringBuilder(
            @"SELECT a.id,a.price,a.description,a.floor_space,a.no_of_badrooms,a.no_of_bathrooms,a.created_date,a.cover_image_url,CONCAT(c.name, ', ', n.name) AS location,p.name AS purpose_name,a.street
             FROM adverts a
             INNER JOIN advert_purposes p ON a.advert_purpose_id = p.id
             INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
             INNER JOIN cities c ON n.city_id = c.id"
            );

        if (noOfBadrooms is not null) countConditions.AppendLine(" AND a.no_of_badrooms = ANY(@noOfBadrooms)");
        if (noOfBathrooms is not null) countConditions.AppendLine(" AND a.no_of_bathrooms = ANY(@noOfBathrooms)");
        if (advertTypeIds is not null) countConditions.AppendLine(" AND a.advert_type_id = ANY(@advertTypeIds)");
        if (advertPurposeIds is not null) countConditions.AppendLine(" AND a.advert_purpose_id = ANY(@advertPurposeIds)");

        if (cityId is not null)
        {
            countAdvertQuery.AppendLine(" INNER JOIN neighborhoods n ON a.neighborhood_id = n.id\r\t     INNER JOIN cities c ON n.city_id = c.id");
            countConditions.AppendLine(" AND c.id = @cityId");
        }
        if (cityId is null && neighborhoodIds is not null)
        {
            countAdvertQuery.AppendLine(" INNER JOIN neighborhoods n ON a.neighborhood_id = n.id\n");
            countConditions.AppendLine(" AND a.neighborhood_id = ANY(@neighborhoodIds)");
        }
        countAdvertQuery.Append(countConditions).Append(';');
        selectAdvertsQuery.Append(countConditions).Append($" ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;");

        return countAdvertQuery.ToString() + selectAdvertsQuery.ToString();
    }

    public const string AllAdvertMapPoints =
        @"SELECT id,latitude,longitude
              FROM adverts";

    public const string SingleAdvertForMapPoint =
       @"SELECT a.id,a.price,a.description,a.floor_space,a.no_of_badrooms,a.no_of_bathrooms,a.created_date,a.cover_image_url,CONCAT(c.name, ', ', n.name) AS location,p.name AS purpose_name,a.street
             FROM adverts a
             INNER JOIN advert_purposes p ON a.advert_purpose_id = p.id
             INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
             INNER JOIN cities c ON n.city_id = c.id
             WHERE a.id = @Id";


    public const string AddAdvertQuery =
       @"INSERT INTO adverts (price,description,floor_space,street,no_of_badrooms,no_of_bathrooms,has_elevator,has_garage,has_terrace,latitude,longitude,has_wifi,is_furnished,created_date,year_of_building_created,cover_image_url,neighborhood_id,building_floor,advert_purpose_id,advert_status_id,advert_type_id,user_id)
            VALUES (@price,@description,@floor_space,@street,@no_of_badrooms,@no_of_bathrooms,@has_elevator,@has_garage,@has_terrace,@latitude,@longitude,@has_wifi,@is_furnished,@created_date,@year_of_building_created,@cover_image_url,@neighborhood_id,@building_floor,@advert_purpose_id,2,@advert_type_id,@user_id)
            RETURNING id";

    public const string UpdateCoverImageQuery =
       @"UPDATE adverts
            SET cover_image_url = @coverImageUrl
            WHERE id = @advertId";


}
