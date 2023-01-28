using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using System.Net.Http.Headers;

namespace Service;
public sealed class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;

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
    public async Task<IEnumerable<string>> UploadMultipleImagesInTempFolder(IFormFileCollection files)
    {
        var imageDataList = new List<string>();
        foreach (var image in files)
        {
            var fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
            var path = Path.Combine(Directory.GetCurrentDirectory(), "temp", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                using (var data = image.OpenReadStream())
                {
                    await data.CopyToAsync(stream);
                    imageDataList.Add(path);
                }
            }
        }
        return imageDataList.AsEnumerable();
    }

    public async Task<string> UploadImageOnCloudinary(string imagePath)
    {

        var uploadResult = new ImageUploadResult();

        using (var fileStream = new FileStream(imagePath, FileMode.Open))
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"fileName", fileStream),
                Transformation = new Transformation().Quality(60),
                Format = "WebP"
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
            File.Delete(imagePath);
        }
        return uploadResult.Url.ToString();
    }

    public async Task<IEnumerable<string>> UploadMultipleImagesOnCloudinary(IEnumerable<string> imagePaths)
    {
        var imagesUrls = new List<string>();
        foreach (var imagePath in imagePaths)
        {
            var uploadResult = new ImageUploadResult();

            using (var fileStream = new FileStream(imagePath, FileMode.Open))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(@"fileName", fileStream),
                    Transformation = new Transformation().Quality(60),
                    Format = "WebP"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            imagesUrls.Add(uploadResult.Url.ToString());
            File.Delete(imagePath);
        }
        return imagesUrls.AsEnumerable();

    }


    public async Task<string> UploadSingleImageInTempFolder(IFormFile image)
    {

        var fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');

        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "temp")))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "temp"));
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "temp", fileName);


        using (var stream = new FileStream(path, FileMode.Create))
        {
            using (var data = image.OpenReadStream())
            {
                await data.CopyToAsync(stream);
            }

        }
        return path;
    }
}
