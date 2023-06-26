using System.Text;

namespace Repository.RawQuery
{
    public static class AgencyQuery
    {
        public const string CreateAgencyQuery = @"
        INSERT INTO agencies (name, created_date, user_id)
        VALUES(@name,@createdDate,@userId);";

        public const string GetAgencyLocatinQuery = @"
        SELECT latitude,longitude,address
        FROM agencies
        WHERE id = @agencyId;";

        public const string GetAgencyImageQuery = @"
        SELECT image_url FROM agencies
        WHERE id = @agencyId;";

        public const string DoesAgencyExistQuery = @"
        SELECT COUNT(id)
        FROM agencies
        WHERE id = @agencyId;";

        public const string GetAgencyByIdQuery = @"
        SELECT name,email,facebook_url,instagram_url,linkedin_url,description,address,twitter_url,website_url,work_start_time,work_end_time,email,image_url
        FROM agencies
        WHERE id = @agencyId;

        SELECT l.id,l.name FROM agency_languages al
        JOIN languages l ON al.language_id = l.id
        WHERE al.agency_id = @agencyId;";


        public const string IsUserAgencyOwnerQuery = @"
        SELECT id
        FROM agencies
        WHERE id = @agencyId
        AND user_id = @userId;";

        public const string CreateAgentQuery = @"
        INSERT INTO agents
        (first_name,last_name,phone_number,image_url,agency_id)
        VALUES
        (@firstName,@lastName,@phoneNumber,@imageUrl,@agencyId);";

        public static string MakeGetAgenciesQuery(string orderBy)
        {
            var countAgenciesQuery = new StringBuilder(@"
            SELECT COUNT(a.id)
            FROM agencies AS a
            WHERE(@name = '' OR LOWER(a.name) LIKE '%' || LOWER(@name) || '%');");

            var selectAgenciesQuery = new StringBuilder($@"
            SELECT a.name, a.created_date, COUNT(*) AS num_adverts, a.email, a.facebook_url, a.instagram_url, a.linkedin_url, a.twitter_url, a.image_url, a.address
            FROM adverts AS ad
            JOIN agents AS ia ON ad.agent_id = ia.id
            JOIN agencies AS a ON ia.agency_id = a.id
            WHERE(@name = '' OR LOWER(a.name) LIKE '%' || LOWER(@name) || '%')
            GROUP BY a.name,a.created_date,a.email,a.facebook_url, a.linkedin_url, a.twitter_url, a.image_url, a.instagram_url, a.address
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;");

            return countAgenciesQuery.ToString() + selectAgenciesQuery.ToString();
        }

        public const string UpdateAgencyQuery = @"
        UPDATE agencies
        SET name = @name, address = @address, description = @description, email =  @email, website_url = @websiteUrl, work_start_time = @workStartTime, work_end_time = @workEndTime, twitter_url = @twitterUrl, facebook_url = @facebookUrl, instagram_url = @instagramUrl,linkedin_url = @linkedinUrl, latitude = @latitude,longitude = @longitude, image_url = @imageUrl
        WHERE id = @agencyId;";

        public const string AssignLanguageToAgencyQuery = @"
        INSERT INTO agency_languages (agency_id, language_id)
        VALUES(@agencyId,@languageId);";


        public const string DeleteLanguagesForAgencyQuery = @"
        DELETE FROM agency_languages
        WHERE agency_id = @agencyId;";

        public const string GetAgencyAdvertsQuery = @"
        SELECT a.id,a.price,a.floor_space,a.no_of_bedrooms,a.no_of_bathrooms,a.created_date,a.cover_image_url,CONCAT(c.name, ', ', n.name) AS location,p.name_en AS purpose_name_en,p.name_sr AS purpose_name_sr,t.name_sr AS type_name_sr,t.name_en AS type_name_en,a.street,a.is_emergency,a.is_under_construction,a.is_furnished
        FROM adverts AS a
        JOIN agents AS ia ON a.agent_id = ia.id
        JOIN advert_types t ON a.type_id = t.id
        JOIN advert_purposes p ON a.purpose_id = p.id
        JOIN neighborhoods n ON a.neighborhood_id = n.id
        JOIN cities c ON n.city_id = c.id
        WHERE ia.agency_id = @agencyId
        AND a.status_id = 1;";

        public const string GetAgentsQuery = @"
        SELECT ia.first_name,ia.last_name,ia.image_url
        FROM agents AS ia
        WHERE ia.agency_id = @agencyId;";
    }
}