namespace Repository.RawQuery;
public static class ImageQuery
{
    public const string InsertImageQuery = @"
        INSERT INTO images (url, advert_id)
        VALUES(@url,@advertId)";

    public const string DeleteImageQuery = @"
        DELETE FROM images
        WHERE url = @url";

    public const string GetPublicIdQuery = @"
        SELECT COUNT(id)
        FROM images
        WHERE url = @url AND advert_id = @advertId";

    public const string GetNumberOfImagesQuery = @"
        SELECT COUNT(id)
        FROM images
        WHERE advert_id = @advertId";
}