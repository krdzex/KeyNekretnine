using Shared.Error;

namespace Entities.DomainErrors;
public static partial class DomainErrors
{
    public static class ImageService
    {
        public static Error ImageCouldntBeDeleted => new Error(
               "ImageService.Failed",
               "Couldnt delete image");
    }
}