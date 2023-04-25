using Shared.RequestFeatures;
using System.Text;

namespace Repository.RawQuery;
public static class AdvertQuery
{
    public const string SingleAdvertQuery = @"
        SELECT a.Id,a.price,a.description_sr,a.description_en,a.floor_space,a.street,a.no_of_bedrooms,a.no_of_bathrooms,a.building_floor,a.has_elevator,a.has_garage,a.has_terrace,a.latitude,a.longitude,a.has_wifi,a.is_furnished,a.created_date,a.year_of_building_created,a.cover_image_url,a.cover_image_blur_url,n.name as neighborhood_name,c.name as city_name, c.id as city_id,p.name_sr AS purpose_name_sr,p.name_en AS purpose_name_en,t.name_sr AS type_name_sr,t.name_en AS type_name_en,CONCAT(u.first_name,' ', u.last_name) AS creator
        FROM adverts a
        INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
        INNER JOIN cities c on n.city_id = c.id
        INNER JOIN advert_purposes p ON a.purpose_id = p.id
        INNER JOIN advert_types t ON a.type_id = t.id
        INNER JOIN asp_net_users u ON a.user_id = u.id
        WHERE a.id = @id
        AND a.status_id = 1;
        
        SELECT url,blur_url FROM images i WHERE  i.advert_id = @id;

        SELECT af.id AS feature_id,af.name FROM advert_features af WHERE af.advert_id = @id";

    public const string SingleAdminAdvertQuery = @"
        SELECT a.Id,a.price,a.description_sr,a.description_en,a.floor_space,a.street,a.no_of_bedrooms,a.no_of_bathrooms,a.building_floor,a.has_elevator,a.has_garage,a.has_terrace,a.latitude,a.longitude,a.has_wifi,a.is_furnished,a.created_date,a.year_of_building_created,a.cover_image_url,n.name as neighborhood_name,c.name as city_name, c.id as city_id,p.name_sr AS purpose_name_sr,p.name_en AS purpose_name_en,t.name_sr AS type_name_sr,t.name_en AS type_name_en,s.name_sr AS status_name_sr,s.name_en AS status_name_en,CONCAT(u.first_name,' ', u.last_name) AS creator
        FROM adverts a
        INNER JOIN images i ON i.advert_id = a.id
        INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
        INNER JOIN cities c on n.city_id = c.id
        INNER JOIN advert_purposes p ON a.purpose_id = p.id
        INNER JOIN advert_types t ON a.type_id = t.id
        INNER JOIN asp_net_users u ON a.user_id = u.id
        INNER JOIN advert_statuses s ON a.status_id = s.id 
        WHERE a.id = @id
        AND a.status_id != 4;

        SELECT url,blur_url FROM images i WHERE  i.advert_id = @id;

        SELECT af.id AS feature_id,af.name FROM advert_features af WHERE af.advert_id = @id";

    public static string MakeGetAdvertQuery(AdvertParameters advertParameters, string orderBy)
    {
        var countConditions = new StringBuilder
            (
            "\t     WHERE a.price >= @minPrice AND a.price <= @maxPrice AND a.status_id = 1 AND a.floor_space >= @minFloorSpace  AND a.floor_space <= @maxFloorSpace "
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
             INNER JOIN advert_purposes p ON a.purpose_id = p.id
             INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
             INNER JOIN cities c ON n.city_id = c.id"
            );

        if (advertParameters.NoOfBedrooms is not null) countConditions.AppendLine(" AND (a.no_of_bedrooms = ANY(@noOfBedrooms) OR ((select get_max_value(@noOfBedrooms)) >= 4 AND a.no_of_bedrooms >= 4))");
        if (advertParameters.NoOfBathrooms is not null) countConditions.AppendLine(" AND (a.no_of_bathrooms = ANY(@noOfBathrooms) OR ((select get_max_value(@noOfBathrooms)) >= 4 AND a.no_of_bathrooms >= 4))");
        if (advertParameters.AdvertTypeIds is not null) countConditions.AppendLine(" AND a.type_id = ANY(@typeIds)");
        if (advertParameters.AdvertPurposeIds is not null) countConditions.AppendLine(" AND a.purpose_id = ANY(@purposeIds)");

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

    public const string AllAdvertMapPoints = @"
        SELECT a.id,a.latitude,a.longitude
        FROM adverts a
        WHERE a.status_id = 1";

    public const string SingleAdvertForMapPoint = @"
        SELECT a.id,a.price,a.floor_space,a.no_of_bedrooms,a.no_of_bathrooms,a.created_date,a.cover_image_url,CONCAT(c.name, ', ', n.name) AS location,p.name_en AS purpose_name_en,p.name_sr AS purpose_name_sr,a.street
        FROM adverts a
        INNER JOIN advert_purposes p ON a.purpose_id = p.id
        INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
        INNER JOIN cities c ON n.city_id = c.id
        WHERE a.id = @Id
        AND a.status_id = 1";


    public const string AddAdvertQuery = @"
        INSERT INTO adverts (price,description_sr,description_en,floor_space,street,no_of_bedrooms,no_of_bathrooms,has_elevator,has_garage,has_terrace,latitude,longitude,has_wifi,is_furnished,created_date,year_of_building_created,cover_image_url,neighborhood_id,building_floor,purpose_id,status_id,type_id,user_id,reference_id)
        VALUES (@price,@description_sr,@description_en,@floor_space,@street,@no_of_bedrooms,@no_of_bathrooms,@has_elevator,@has_garage,@has_terrace,@latitude,@longitude,@has_wifi,@is_furnished,@created_date,@year_of_building_created,@cover_image_url,@neighborhood_id,@building_floor,@purpose_id,4,@type_id,@user_id,@reference_id)
        RETURNING id";

    public const string UpdateCoverImageQuery = @"
        UPDATE adverts
        SET cover_image_url = @coverImageUrl
        WHERE id = @advertId";

    public const string UpdateAdvertStatus =
       @"UPDATE adverts
            SET status_id = 2
            WHERE id = @advertId";

    public const string AdvertExistQuery =
        @"SELECT COUNT(*)
          FROM adverts
          WHERE id = @AdvertId";


    public const string AdvertExistAndApprovedQuery =
        @"SELECT COUNT(*)
          FROM adverts
          WHERE id = @AdvertId
          AND status_id = 1";

    public const string ApproveAdvertQuery =
        @"UPDATE adverts
          SET status_id = 1
          WHERE id = @AdvertId";

    public const string DeclineAdvertQuery =
        @"UPDATE adverts
          SET status_id = 3
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
             INNER JOIN advert_purposes p ON a.purpose_id = p.id
             INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
             INNER JOIN cities c ON n.city_id = c.id
             INNER JOIN advert_statuses s ON a.status_id = s.id "
            );

        if (advertStatusIds is not null) countConditions.AppendLine(" WHERE a.status_id = ANY(@advertStatusIds)");

        countAdvertQuery.Append(countConditions).Append(';');
        selectAdvertsQuery.Append(countConditions).Append($" ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;");

        return countAdvertQuery.ToString() + selectAdvertsQuery.ToString();
    }

    public const string GetUserEmailFromAdvertIdQuery = @"
        SELECT u.email
        FROM adverts a
        JOIN asp_net_users u ON u.id = a.user_id
        WHERE a.id = @advertId";


    public static string GetMyAdverts(string orderBy) => @$"
        SELECT COUNT(a.id)
        FROM adverts a
        WHERE a.user_id = @userId AND a.status_id != 4;

        SELECT a.id,a.created_date,a.cover_image_url,CONCAT(c.name, ', ', n.name) AS location,p.name_en AS purpose_name_en,p.name_sr AS purpose_name_sr,t.name_en AS type_name_en,t.name_sr AS type_name_sr,a.description_sr,a.description_en
        FROM adverts a
        INNER JOIN advert_purposes p ON a.purpose_id = p.id
        INNER JOIN advert_types t ON a.type_id = t.id
        INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
        INNER JOIN cities c ON n.city_id = c.id
        WHERE a.user_id = @userId AND a.status_id != 4
        ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;";

    public const string MakeAdvertFavoriteQuery = @"
          INSERT INTO user_advert_favorites(user_id,advert_id,created_favorite_date)
          VALUES(@userId,@advertId,@createdFavoriteDate)";

    public const string ChackIfAdvertIsFavoriteQuery = @"
          SELECT COUNT(*)
          FROM user_advert_favorites
          WHERE user_id = @userId
          AND advert_id = @advertId";

    public const string DeleteAdvertFromFavoriteQuery = @"
          DELETE FROM user_advert_favorites
          WHERE user_id = @userId
          AND advert_id = @advertId";

    public const string GetFavoriteAdverts = @"
         SELECT COUNT(a.id)
         FROM user_advert_favorites ua
         INNER JOIN adverts a ON a.id = ua.advert_id
         WHERE ua.user_id = @userId;

         SELECT a.id,a.price,a.floor_space,a.no_of_bedrooms,a.no_of_bathrooms,a.created_date,a.cover_image_url,a.cover_image_blur_url,CONCAT(c.name, ', ', n.name) AS location,p.name_en AS purpose_name_en,p.name_sr AS purpose_name_sr,a.street
         FROM user_advert_favorites ua
         INNER JOIN adverts a ON a.id = ua.advert_id
         INNER JOIN advert_purposes p ON a.purpose_id = p.id
         INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
         INNER JOIN cities c ON n.city_id = c.id
         WHERE ua.user_id = @userId
         ORDER BY created_favorite_date DESC OFFSET @Skip FETCH NEXT @Take ROWS ONLY;";

    public const string ReportAdvertQuery = @"
        INSERT INTO user_advert_reports (user_id,advert_id,reject_reason_id,created_report_date)
        VALUES (@userId,@advertId,@rejectReasonId,@createdReportDate)";

    public const string ChackIfAdvertIsAlreadyReportedQuery = @"
        SELECT COUNT(*)
        FROM user_advert_reports
        WHERE user_id = @userId AND advert_id = @advertId AND reject_reason_id = @rejectReasonId;";

    public const string GetReportsQuery = @"
        SELECT COUNT(*)
        FROM user_advert_reports;

        SELECT 
        advert_id,
        SUM(CASE WHEN reject_reason_id = 1 THEN 1 ELSE 0 END) AS repeating_advert,
        SUM(CASE WHEN reject_reason_id = 2 THEN 1 ELSE 0 END) AS bad_images,
        SUM(CASE WHEN reject_reason_id = 3 THEN 1 ELSE 0 END) AS bad_informations,
        COUNT(advert_id) AS all_reports
        FROM user_advert_reports
        GROUP BY advert_id
        ORDER BY COUNT(advert_id) DESC OFFSET @Skip FETCH NEXT @Take ROWS ONLY;";

    public const string GetCompareAdvertsQuery = @"
        SELECT a.Id,a.price,a.description_sr,a.description_en,a.floor_space,a.street,a.no_of_bedrooms,a.no_of_bathrooms,a.building_floor,a.has_elevator,a.has_garage,a.has_terrace,a.latitude,a.longitude,a.has_wifi,a.is_furnished,a.created_date,a.year_of_building_created,a.cover_image_url,n.name as neighborhood_name,c.name as city_name, c.id as city_id,p.name_sr AS purpose_name_sr,p.name_en AS purpose_name_en,t.name_sr AS type_name_sr,t.name_en AS type_name_en,CONCAT(u.first_name,' ', u.last_name) AS creator
        FROM adverts a
        INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
        INNER JOIN cities c on n.city_id = c.id
        INNER JOIN advert_purposes p ON a.purpose_id = p.id
        INNER JOIN advert_types t ON a.type_id = t.id
        INNER JOIN asp_net_users u ON a.user_id = u.id
        WHERE a.id = ANY(@advertsId)
        AND a.status_id = 1";

    public const string UpdateAdvertInformationsQuery = @"
        UPDATE adverts SET 
        price = @price,
        description_sr = @description_sr,
        description_en = @description_en,
        floor_space = @floor_space,
        no_of_bedrooms = @no_of_bedrooms,
        no_of_bathrooms = @no_of_bathrooms,
        building_floor = @building_floor,
        has_elevator = @has_elevator,
        has_garage = @has_garage,
        has_terrace = @has_terrace,
        has_wifi = @has_wifi,
        is_furnished = @is_furnished,
        year_of_building_created = @year_of_building_created,
        purpose_id = @purpose_id,
        type_id = @type_id,
        status_id = 
            CASE 
                WHEN status_id = 3 THEN 2
                ELSE status_id
            END
        WHERE id = @advertId
        AND status_id != 4";

    public const string ChackIfUserIsAdvertOwnerQuery = @"
        SELECT COUNT(*)
        FROM adverts a
        INNER JOIN asp_net_users u ON a.user_id = u.id
        WHERE a.id = @advertId and u.email = @email";

    public const string UpdateAdvertLocationQuery = @"
        UPDATE adverts SET 
        latitude = @latitude,
        longitude = @longitude,
        street = @street,
        neighborhood_id = @neighborhoodId,
        status_id = 
            CASE  
                WHEN status_id = 3 THEN 2
                ELSE status_id
            END
        WHERE id = @advertId
        AND status_id != 4";

    public const string MyAdvertQuery = @"
        SELECT a.Id,a.price,a.description_sr,a.description_en,a.floor_space,a.street,a.no_of_bedrooms,a.no_of_bathrooms,a.building_floor,a.has_elevator,a.has_garage,a.has_terrace,a.latitude,a.longitude,a.has_wifi,a.is_furnished,a.created_date,a.year_of_building_created,a.cover_image_url,a.cover_image_blur_url,n.name as neighborhood_name,c.name as city_name, c.id as city_id,a.neighborhood_id,p.name_sr AS purpose_name_sr,p.name_en AS purpose_name_en,t.name_sr AS type_name_sr,t.name_en AS type_name_en,CONCAT(u.first_name,' ', u.last_name) AS creator, s.name_sr AS status_name_sr,s.name_en AS status_name_en
        FROM adverts a
        INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
        INNER JOIN cities c on n.city_id = c.id
        INNER JOIN advert_purposes p ON a.purpose_id = p.id
        INNER JOIN advert_types t ON a.type_id = t.id
        INNER JOIN asp_net_users u ON a.user_id = u.id
        INNER JOIN advert_statuses s ON a.status_id = s.id
        WHERE a.id = @id AND a.user_id = @userId AND a.status_id != 4;
        
        SELECT url,blur_url FROM images i WHERE  i.advert_id = @id;

        SELECT af.id AS feature_id,af.name FROM advert_features af WHERE af.advert_id = @id";
}