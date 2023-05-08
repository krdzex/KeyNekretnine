using System.Reflection;
using System.Text;

namespace Shared.RequestFeatures;
public static class OrderQueryBuilder
{
    public static string CreateOrderQuery<T>(string orderByQueryString, char alias)
    {
        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(T)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        for (var i = 0; i < orderParams.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(orderParams[i]))
                continue;

            var propertyFromQueryName = orderParams[i].Split(" ")[0];
            var objectProperty = propertyInfos
                .FirstOrDefault(pi => pi.Name
                    .Equals(propertyFromQueryName,
                        StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var direction = orderParams[i].EndsWith(" asc") ? "asc" : "desc";

            if (propertyFromQueryName.ToLower() == "num_adverts")
            {
                orderQueryBuilder
                    .Append($"{objectProperty.Name.ToLower().ToString()} {direction}, ");
            }
            else
            {
                orderQueryBuilder
                    .Append($"{alias}.{objectProperty.Name.ToLower().ToString()} {direction}, ");
            }

        }
        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

        return string.IsNullOrEmpty(orderQuery) ? "a.created_date desc" : orderQuery;
    }
}