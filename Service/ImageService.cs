using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using Service.Contracts;

namespace Service;
public sealed class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;
    private static readonly RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager(100 * 1024, 50 * 1024 * 1024);

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

    public async Task<IEnumerable<byte[]>> UploadMultipleImagesInTempFolder(IFormFileCollection images)
    {
        var imageDataList = new List<byte[]>();
        foreach (var image in images)
        {
            using (var memoryStream = manager.GetStream("test2"))
            {
                await image.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                var buffer = new byte[memoryStream.Length];
                memoryStream.Read(buffer, 0, buffer.Length);
                imageDataList.Add(buffer);

            }
        }
        return imageDataList.AsEnumerable();
    }

    public async Task<string> UploadImageOnCloudinary(byte[] imageData)
    {
        using (var memoryStream = manager.GetStream("memory2", imageData, 0, imageData.Length))
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"fileName", memoryStream),
                Transformation = new Transformation().Quality(60),
                Format = "WebP"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }
    }

    public async Task<IEnumerable<string>> UploadMultipleImagesOnCloudinary(IEnumerable<byte[]> imagesData)
    {
        var imagesUrls = new List<string>();
        foreach (var imageData in imagesData)
        {
            using (var memoryStream = manager.GetStream("memory3", imageData, 0, imageData.Length))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(@"fileName", memoryStream),
                    Transformation = new Transformation().Quality(60),
                    Format = "WebP"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                imagesUrls.Add(uploadResult.Url.ToString());
            }
        }
        return imagesUrls.AsEnumerable();
    }


    public async Task<byte[]> UploadSingleImageInTempFolder(IFormFile image)
    {
        using (var memoryStream = manager.GetStream("test"))
        {
            await image.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var buffer = new byte[memoryStream.Length];
            memoryStream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
