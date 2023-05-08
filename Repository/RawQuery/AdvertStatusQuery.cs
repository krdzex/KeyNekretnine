namespace Repository.RawQuery;
public class AdvertStatusQuery
{
    public const string AllAdvertStatuses = @"
        SELECT * from advert_statuses WHERE id != 4";
}
