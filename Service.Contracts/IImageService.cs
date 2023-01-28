using Microsoft.AspNetCore.Http;

namespace Service.Contracts;
public interface IImageService
{
    Task<IEnumerable<string>> UploadMultipleImagesInTempFolder(IFormFileCollection files);
    Task<IEnumerable<string>> UploadMultipleImagesOnCloudinary(IEnumerable<string> imagePaths);

    Task<string> UploadImageOnCloudinary(string imagePath);
    Task<string> UploadSingleImageInTempFolder(IFormFile image);
}

