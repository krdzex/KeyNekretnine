using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using KeyNekretnine.Application.Abstraction.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;

namespace KeyNekretnine.Infrastructure.ImageProvider;
internal sealed class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;
    private static readonly RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager(new RecyclableMemoryStreamManager.Options()
    {
        BlockSize = 1024,
        LargeBufferMultiple = 1024 * 1024,
        MaximumBufferSize = 16 * 1024 * 1024,
        GenerateCallStacks = true,
        AggressiveBufferReturn = true,
        MaximumLargePoolFreeBytes = 16 * 1024 * 1024 * 4,
        MaximumSmallPoolFreeBytes = 100 * 1024,
    });

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

    public async Task<string> UploadImageOnCloudinary(IFormFile image)
    {
        using (var memoryStream = manager.GetStream("test"))
        {
            await image.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"fileName", memoryStream),
                Transformation = new Transformation().Quality(60),
                Format = "WebP",
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }
    }

    public async Task<string?> DeleteImageFromCloudinary(string publicId)
    {
        var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId));

        return result.Error?.Message;
    }

    public string GetPublicIdFromUrl(string url)
    {
        string[] urlParts = url.Split('/');
        string part8 = string.Join("/", urlParts.Skip(7));
        int dotIndex = part8.LastIndexOf('.');
        string publicId = part8.Substring(0, dotIndex);

        return publicId;
    }
}