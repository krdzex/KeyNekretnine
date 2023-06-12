using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using Service.Contracts;

namespace Service;
internal sealed class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;
    private static readonly RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager(128 * 1024, 13 * 1024 * 1024);

    public ImageService()
    {
        var account = new Account
           (
            Environment.GetEnvironmentVariable("CLOUD_NAME"),
            Environment.GetEnvironmentVariable("CLOUD_API_KEY"),
            Environment.GetEnvironmentVariable("CLOUD_API_SECRET")
           );
        _cloudinary = new Cloudinary(account);
    }


    public async Task<string> UploadImageOnCloudinaryUsingDb(byte[] item, string folderName)
    {
        using (var memoryStream = manager.GetStream("memory", item, 0, item.Length))
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"fileName", memoryStream),
                Transformation = new Transformation().Quality(60),
                Format = "WebP",
                Folder = folderName
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }
    }

    public async Task<string> UploadImageOnCloudinary(IFormFile image, string folderName)
    {
        using (Stream stream = new MemoryStream())
        {
            stream.Flush();
            await image.CopyToAsync(stream);
            stream.Position = 0;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"fileName", stream),
                Transformation = new Transformation().Quality(60),
                Format = "WebP",
                Folder = folderName
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }
    }

    public async Task<DeletionResult> DeleteImageFromCloudinary(string publicId)
    {
        var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId));

        return result;
    }
}