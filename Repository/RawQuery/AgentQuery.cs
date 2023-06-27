using System.Text;

namespace Repository.RawQuery;
public class AgentQuery
{
    public const string CreateAgentQuery = @"
        INSERT INTO agents
        (first_name, last_name, phone_number, image_url, agency_id, description, email, twitter_url, facebook_url, instagram_url, linkedin_url)
        VALUES
        (@firstName, @lastName, @phoneNumber, @imageUrl, @agencyId, @description, @email, @twitterUrl, @facebookUrl, @instagramUrl, @linkedinUrl)
        RETURNING id;";

    public static string MakeGetAgentsQuery(string orderBy)
    {
        var countAgentsQuery = new StringBuilder(@"
            SELECT COUNT(a.id)
            FROM agents AS a;");

        var selectAgentsQuery = new StringBuilder($@"
            SELECT a.id, a.first_name, COUNT(ad.id) AS num_adverts, a.last_Name, a.email, a.twitter_Url, a.facebook_Url, a.instagram_Url, a.linkedin_Url, a.linkedin_Url, a.image_Url, ag.id AS agency_id, ag.name AS agency_name 
            FROM agents AS a
            LEFT JOIN adverts AS ad ON a.id = ad.agent_id AND ad.status_id = 1
            LEFT JOIN agencies as ag ON ag.id = a.agency_id
            GROUP BY a.id, ag.id
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;");

        return countAgentsQuery.ToString() + selectAgentsQuery.ToString();
    }

    public const string GetAgentAdvertsQuery = @"
        SELECT a.id,a.price,a.floor_space,a.no_of_bedrooms,a.no_of_bathrooms,a.created_date,a.cover_image_url,CONCAT(c.name, ', ', n.name) AS location,p.name_en AS purpose_name_en,p.name_sr AS purpose_name_sr,t.name_sr AS type_name_sr,t.name_en AS type_name_en,a.street,a.is_emergency,a.is_under_construction,a.is_furnished
        FROM adverts AS a
        JOIN advert_types t ON a.type_id = t.id
        JOIN advert_purposes p ON a.purpose_id = p.id
        JOIN neighborhoods n ON a.neighborhood_id = n.id
        JOIN cities c ON n.city_id = c.id
        WHERE a.agent_id = @agentId
        AND a.status_id = 1;";

    public const string GetAgentByIdQuery = @"
        SELECT a.id, a.first_name, a.last_Name, a.email, a.twitter_Url, a.facebook_Url, a.instagram_Url, a.linkedin_Url, a.linkedin_Url, a.image_Url, ag.id AS agency_id, ag.name AS agency_name, a.description , a.phone_number
        FROM agents AS a
        LEFT JOIN agencies as ag ON ag.id = a.agency_id
        WHERE a.id = @agentId;

        SELECT l.id,l.name FROM agent_languages al
        JOIN languages l ON al.language_id = l.id
        WHERE al.agent_id = @agentId;";

    public const string AssignLanguageToAgentQuery = @"
        INSERT INTO agent_languages (agent_id, language_id)
        VALUES(@agentId,@languageId);";


    public const string DeleteLanguagesForAgentQuery = @"
        DELETE FROM agent_languages
        WHERE agent_id = @agentId;";

    public const string GetAgentImageQuery = @"
        SELECT image_url FROM agents
        WHERE id = @agentId;";

    public const string UpdateAgentQuery = @"
        UPDATE agents
        SET first_name = @firstName, last_name = @lastName, phone_number = @phoneNumber, image_url = @imageUrl, description = @description, email = @email, twitter_url = @twitterUrl, facebook_url = @facebookUrl, instagram_url = instagram_url, linkedin_url = @linkedinUrl
        WHERE id = @agentId;";

    public const string DoesAgentExistQuery = @"
        SELECT COUNT(id)
        FROM agents
        WHERE id = @agentId;";

    public const string GetAgencyIdFromUserQuery = @"
        SELECT agency_id
        FROM agents
        WHERE id = @agentId;";
}
