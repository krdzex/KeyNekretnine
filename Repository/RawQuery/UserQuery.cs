using System.Text;

namespace Repository.RawQuery;
public class UserQuery
{
    public static string MakeUsersQuery(bool? isBanned, string orderBy)
    {

        var countConditions = new StringBuilder("\t    WHERE(@username = '' OR LOWER(u.User_name) LIKE '%' || LOWER(@username) || '%') ");
        var countAdvertQuery = new StringBuilder(@"
            SELECT COUNT(u.id)
            FROM asp_net_users AS u");

        var selectAdvertsQuery = new StringBuilder(@"
            SELECT u.Id, u.Email, u.User_name, u.account_created_date,
                CASE
                    WHEN u.is_banned = true AND u.ban_end >= now() 
                        THEN true 
                    ELSE false 
                END AS is_banned,
            COALESCE(a.num_adverts, 0) AS num_adverts
            FROM asp_net_users AS u
            LEFT JOIN (
            SELECT user_id, COUNT(*) AS num_adverts
            FROM adverts
            GROUP BY user_id
            ) AS a ON u.Id = a.user_id");

        if (isBanned != null)
        {
            if (isBanned == true)
            {
                countConditions.AppendLine("   AND (is_banned = true AND u.ban_end > now())");
            }
            else
            {
                countConditions.AppendLine("  AND (u.is_banned = false OR (u.is_banned = true AND u.ban_end < now()))");
            }
        }

        countAdvertQuery.Append(countConditions).Append(';');
        selectAdvertsQuery.Append(countConditions).Append($"ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;");

        return countAdvertQuery.ToString() + selectAdvertsQuery.ToString();
    }


    public const string GetUserIdFromEmailQuery = @"
        SELECT u.Id
        FROM asp_net_users AS u
        WHERE email = @email";

    public const string GetLoggedUserInformations = @"
        SELECT u.email,u.first_name, u.last_name,u.Account_Created_Date,u.Phone_Number,u.About,u.Profile_Image_Url,u.email_confirmed
        FROM asp_net_users AS u
        WHERE email = @email";

    public const string IsUserBannedQuery = @"
        SELECT 
            CASE 
                WHEN u.is_banned = false OR (u.is_banned = true AND u.ban_end < now())
                    THEN false
                    ELSE true
            END AS is_banned
        FROM asp_net_users u
        WHERE u.email = @email";

    public const string GetUserById = @"
        SELECT u.first_name,u.last_name,u.account_created_date,u.id, u.email,
            CASE
                WHEN u.is_banned = true AND u.ban_end >= now() 
                    THEN true 
                    ELSE false 
            END AS is_banned
        FROM asp_net_users AS u
        WHERE id = @id";

    public const string GetEmailAndBanEndDateFromUserId = @"
        SELECT u.email, u.ban_end
        FROM asp_net_users AS u
        WHERE id = @id";

    public const string GetEmailFromUserId = @"
        SELECT u.email
        FROM asp_net_users AS u
        WHERE id = @id";

}