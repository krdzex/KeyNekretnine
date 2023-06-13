using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Service.Contracts;
public interface IImageService
{
    Task<string> UploadImageOnCloudinaryUsingDb(byte[] imagePath, string folderName);
    Task<string> UploadImageOnCloudinary(IFormFile image);
    Task<DeletionResult> DeleteImageFromCloudinary(string publicId);
    string GetPublicIdFromUrl(string url);
}