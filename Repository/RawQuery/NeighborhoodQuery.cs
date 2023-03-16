namespace Repository.RawQuery;
public class NeighborhoodQuery
{
    public const string NeighborhoodForCity = @"
        SELECT * FROM neighborhoods
        WHERE city_id = @id";
}