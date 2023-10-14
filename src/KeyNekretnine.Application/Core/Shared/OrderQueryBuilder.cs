using System.Reflection;
using System.Text;

namespace KeyNekretnine.Application.Core.Shared;
public static class OrderQueryBuilder
{
    public static string CreateOrderQuery<T>(string orderByQueryString)
    {
        var propertyInfos = typeof(T)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(orderByQueryString))
        {
            var propertyFromQueryName = orderByQueryString.Split(" ")[0];
            var objectProperty = propertyInfos
                .FirstOrDefault(pi => pi.Name
                    .Equals(propertyFromQueryName,
                        StringComparison.InvariantCultureIgnoreCase));


            if (objectProperty is not null)
            {
                var direction = orderByQueryString.EndsWith(" asc") ? "asc" : "desc";

                orderQueryBuilder
                        .Append($"{objectProperty.Name.ToLower().ToString()} {direction}");
            }
        }

        var orderQuery = orderQueryBuilder.ToString();

        return string.IsNullOrEmpty(orderQuery) ? "createdOnDate desc" : orderQuery;
    }
}
