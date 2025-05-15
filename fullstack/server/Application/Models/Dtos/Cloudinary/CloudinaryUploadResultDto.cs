namespace Application.Models.Dtos.Cloudinary;

public class CloudinaryUploadResultDto
{
    public string PublicId { get; set; } = null!;
    public string SecureUrl { get; set; } = null!;
}