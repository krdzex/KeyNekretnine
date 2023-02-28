namespace Repository.RawQuery;
public class CityQuery
{
    public const string AllCities =
     @"SELECT * FROM cities";

    public const string CitiesWithMostAdverts =
      @"WITH cte AS (
          SELECT 
            c.id,
            c.name, 
            c.image_url, 
            count(a.id) AS adverts_count
          FROM adverts a
          INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
          INNER JOIN cities c ON c.id = n.city_id
          WHERE a.advert_status_id = 1
          GROUP BY c.name, c.image_url,c.id
        )
        SELECT 
          id,
          name, 
          adverts_count, 
          image_url
        FROM cte
        ORDER BY adverts_count DESC
        LIMIT 8";
}
