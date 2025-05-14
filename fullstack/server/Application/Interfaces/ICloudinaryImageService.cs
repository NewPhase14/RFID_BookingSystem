namespace Application.Interfaces;

public interface ICloudinaryImageService
{
    Task<string> UploadImageAsync(Stream fileStream, string fileName);
}