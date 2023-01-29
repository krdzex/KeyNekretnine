using Microsoft.AspNetCore.Http;

namespace Service.Contracts;
public interface IImageService
{
    Task<IEnumerable<byte[]>> UploadMultipleImagesInTempFolder(IFormFileCollection files);
    Task<IEnumerable<string>> UploadMultipleImagesOnCloudinary(IEnumerable<byte[]> imagePaths);

    Task<string> UploadImageOnCloudinary(byte[] imagePath);
    Task<byte[]> UploadSingleImageInTempFolder(IFormFile image);
}

