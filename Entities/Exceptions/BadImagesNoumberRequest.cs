namespace Entities.Exceptions;
public sealed class BadImagesNoumberRequest : BadRequestException
{
    public BadImagesNoumberRequest()
        : base("Advert need at least 4 images.")
    {
    }
}
