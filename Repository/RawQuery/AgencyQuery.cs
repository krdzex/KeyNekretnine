using System.Text;

namespace Repository.RawQuery
{
    public static class AgencyQuery
    {
        public const string CreateAgencyQuery = @"
        INSERT INTO agencies (name, created_date, user_id)
        VALUES(@name,@createdDate,@userId);";

        public const string GetAgencyImageQuery = @"
        SELECT image_url FROM agencies
        WHERE id = @agencyId;";

        public const string DoesAgencyExistQuery = @"
        SELECT COUNT(id)
        FROM agencies
        WHERE id = @agencyId;";

        public const string GetAgencyByIdQuery = @"
        SELECT name,email,facebook_url,instagram_url,latitude,longitude,linkedin_url,description,location,twitter_url,website_url,work_start_time,work_end_time,email,image_url
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
        INSERT INTO imaginary_agents
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
            SELECT a.name,a.created_date, COUNT(*) AS num_adverts
            FROM adverts AS ad
            JOIN imaginary_agents AS ia ON ad.imaginary_agent_id = ia.id
            JOIN agencies AS a ON ia.agency_id = a.id
            WHERE(@name = '' OR LOWER(a.name) LIKE '%' || LOWER(@name) || '%')
            GROUP BY a.name,a.created_date
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;");

            return countAgenciesQuery.ToString() + selectAgenciesQuery.ToString();
        }

        public const string UpdateAgencyQuery = @"
        UPDATE agencies
        SET name = @name, location = @location, description = @description, email =  @email, website_url = @websiteUrl, work_start_time = @workStartTime, work_end_time = @workEndTime, twitter_url = @twitterUrl, facebook_url = @facebookUrl, instagram_url = @instagramUrl,linkedin_url = @linkedinUrl, latitude = @latitude,longitude = @longitude, image_url = @imageUrl
        WHERE id = @agencyId;";

        public const string AssignLanguageToAgencyQuery = @"
        INSERT INTO agency_languages (agency_id, language_id)
        VALUES(@agencyId,@languageId);";


        public const string DeleteLanguagesForAgencyQuery = @"
        DELETE FROM agency_languages
        WHERE agency_id = @agencyId;";
    }
}