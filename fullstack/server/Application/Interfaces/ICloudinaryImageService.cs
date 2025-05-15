using Application.Models.Dtos.Cloudinary;

namespace Application.Interfaces;

public interface ICloudinaryImageService
{
    Task<CloudinaryUploadResultDto> UploadImageAsync(Stream fileStream, string fileName);
    
    void DeleteImageAsync(string publicId);
    
}