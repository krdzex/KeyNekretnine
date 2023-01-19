namespace Repository.RawQuery;
public class UserQuery
{
    public const string GetUsersQuery =
    @"SELECT COUNT(u.id)
      FROM ""AspNetUsers"" AS u
      WHERE (@username = '' OR LOWER(u.User_name) LIKE '%' || LOWER(@username) || '%')
      AND (u.is_banned = false OR (u.is_banned = true AND u.ban_end < now()));

      SELECT u.Id, u.Email, u.User_name
      FROM ""AspNetUsers"" AS u
      WHERE (@username = '' OR LOWER(u.User_name) LIKE '%' || LOWER(@username) || '%')
      AND (u.is_banned = false OR (u.is_banned = true AND u.ban_end < now()))
      ORDER BY u.User_name
      OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

    public const string GetBannedUsersQuery =
    @"SELECT COUNT(u.id)
      FROM ""AspNetUsers"" AS u
      WHERE (@username = '' OR LOWER(u.User_name) LIKE '%' || LOWER(@username) || '%')
      AND (is_banned = true AND u.ban_end > now());

      SELECT u.Id, u.Email, u.User_name
      FROM ""AspNetUsers"" AS u
      WHERE (@username = '' OR LOWER(u.User_name) LIKE '%' || LOWER(@username) || '%')
      AND (is_banned = true AND u.ban_end > now())
      ORDER BY u.User_name
      OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
}
