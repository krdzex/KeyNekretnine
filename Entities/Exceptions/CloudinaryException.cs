namespace Entities.Exceptions;
public sealed class CloudinaryException : BadRequestException
{
    public CloudinaryException()
        : base("Bad request coused error with image service")
    {
    }
}
