namespace Entities.Exceptions;
public class NeighborhoodsNotFoundException : NotFoundException
{
    public NeighborhoodsNotFoundException(int cityId)
        : base($"Neighborhoods for city id: {cityId} doesn't exist in the database.")
    {
    }
}