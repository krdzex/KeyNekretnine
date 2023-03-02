namespace Repository.RawQuery;
public static class ImageQuery
{
    public const string InsertImageQuery =
    @"INSERT INTO images (url, advert_id)
          VALUES(@url,@advertId)";
}