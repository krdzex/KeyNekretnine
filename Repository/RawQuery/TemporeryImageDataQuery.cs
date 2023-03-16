namespace Repository.RawQuery;
public static class TemporeryImageDataQuery
{
    public const string GetAdvertPurposesQuery = @"
        SELECT image_data
        FROM temporery_images_data
        WHERE advert_id = @advert_id AND is_cover = @is_cover";

    public const string InsertTemporeryImageDataQuery = @"
        INSERT INTO temporery_images_data (image_data, advert_id,is_cover)
        VALUES (@image_data,@advert_id,@is_cover)";

    public const string DeleteTemporeryImageDataQuery = @"
        DELETE FROM temporery_images_data
        WHERE advert_id = @advert_id";
}