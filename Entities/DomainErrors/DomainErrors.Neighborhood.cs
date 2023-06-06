using Shared.Error;

namespace Entities.DomainErrors;
public static partial class DomainErrors
{
    public static class Neighborhood

    {
        public static Error NeighborhoodNotFound => new Error(
               "Neighborhood.NotFound",
               "Neighboorhoods for your city are not found");
    }
}