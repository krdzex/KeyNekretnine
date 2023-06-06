namespace Entities.Exceptions;
public sealed class ImageNotFoundException : NotFoundException
{
    public ImageNotFoundException()
        : base("Image is not found")
    {
    }
}
