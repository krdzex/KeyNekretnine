namespace Repository.RawQuery;
public class AdvertFeatureQuery
{
    public const string InsertFeatureQuery = @"
         INSERT INTO advert_features(name,advert_id) VALUES(@name,@advert_id)";
}
