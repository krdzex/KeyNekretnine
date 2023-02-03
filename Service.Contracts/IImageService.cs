namespace Service.Contracts;
public interface IImageService
{
    Task<string> UploadImageOnCloudinary(byte[] imagePath);
}

