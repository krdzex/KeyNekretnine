using System.Text;

namespace Repository.RawQuery;
public class UserQuery
{
    public static string MakeUsersQuery(bool? isBanned)
    {

        var countConditions = new StringBuilder("\t    WHERE(@username = '' OR LOWER(u.User_name) LIKE '%' || LOWER(@username) || '%') ");
        var countAdvertQuery = new StringBuilder(
            @"SELECT COUNT(u.id)
             FROM ""AspNetUsers"" AS u");

        var selectAdvertsQuery = new StringBuilder(
            @"SELECT u.Id, u.Email, u.User_name,
                    CASE
                        WHEN u.is_banned = true AND u.ban_end >= now() 
                            THEN true 
                        ELSE false 
                    END AS is_banned
             FROM ""AspNetUsers"" AS u");
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
        selectAdvertsQuery.Append(countConditions).Append($" ORDER BY u.User_name OFFSET @Skip FETCH NEXT @Take ROWS ONLY;");

        return countAdvertQuery.ToString() + selectAdvertsQuery.ToString();
    }


    public const string GetUserIdFromEmailQuery =
    @"SELECT u.Id
      FROM ""AspNetUsers"" AS u
      WHERE email = @email";

    public const string GetLoggedUserInformations =
    @"SELECT u.email,u.first_name, u.last_name
      FROM ""AspNetUsers"" AS u
      WHERE email = @email";

    public const string IsUserBannedQuery =
    @"SELECT 
        CASE 
            WHEN u.is_banned = false OR (u.is_banned = true AND u.ban_end < now()) THEN false
            ELSE true
        END AS is_banned
      FROM ""public"".""AspNetUsers"" u
      WHERE u.email = @email";

}