namespace Repository.RawQuery;
public class UserQuery
{
    public const string BanExpiredQuery =
    @"UPDATE ""AspNetUsers""
     SET ban_end = NULL, is_banned = false
     WHERE id = '278965bd-a6f2-490c-aab4-71bc67bdd293' AND concurrency_stamp = '60cfe688-d5fa-4872-8ecc-18b34a34a7ff'";

}
