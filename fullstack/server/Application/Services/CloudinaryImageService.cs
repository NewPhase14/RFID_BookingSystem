using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Application.Services;

public class CloudinaryImageService(Cloudinary cloudinary) : ICloudinaryImageService
{
    // Uploads an image asynchronously to Cloudinary and returns the image URL (URL is the link to the uploaded image)
    public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileStream),
            Transformation = new Transformation().Width(500).Height(500).Crop("fill"),
            // PublicId is the unique identifier for the image in Cloudinary (same as "filename")
            PublicId = fileName
        };

        if (uploadParams == null)
        {
            throw new ArgumentNullException(nameof(uploadParams));
        }
        
        var uploadResult = await cloudinary.UploadAsync(uploadParams);
        if (uploadResult == null)
        {
            throw new InvalidOperationException("Upload failed");
        }
        
        // Return the secure HTTPS URL of the uploaded image
        return uploadResult.SecureUrl.ToString();
    }
        
}
