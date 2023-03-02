namespace Entities.Exceptions;
public sealed class BadFloorSpaceException : BadRequestException
{
    public BadFloorSpaceException()
        : base("Max floor space can't be less than min floor space.")
    {
    }
}