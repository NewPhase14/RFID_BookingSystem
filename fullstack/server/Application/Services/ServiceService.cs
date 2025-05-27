using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.Service;
using Core.Domain.Entities;

namespace Application.Services;

public class ServiceService(IServiceRepository serviceRepository, ICloudinaryImageService cloudinaryImageService)
    : IServiceService
{
    public async Task<ServiceResponseDto> CreateService(ServiceCreateRequestDto dto)
    {
        var imageUrl = string.Empty;
        var publicId = string.Empty;

        // This allows inserting a base64 string with the request for testing purposes,
        // This should be changed to a file upload in the future

        if (!string.IsNullOrEmpty(dto.ImageUrl))
        {
            // Convert Base64 string to byte array
            var imageBytes = Convert.FromBase64String(dto.ImageUrl);
            // Create a memory stream from the image bytes
            using var stream = new MemoryStream(imageBytes);
            // Generate a unique filename for the image
            var fileName = Guid.NewGuid().ToString();
            // Upload the image stream to Cloudinary and get the image URL
            var uploadResult = await cloudinaryImageService.UploadImageAsync(stream, fileName);
            imageUrl = uploadResult.SecureUrl;
            publicId = uploadResult.PublicId;
        }

        var service = new Service
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = imageUrl,
            PublicId = publicId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var createdService = await serviceRepository.CreateService(service);
        return new ServiceResponseDto
        {
            Id = createdService.Id,
            Name = createdService.Name,
            Description = createdService.Description,
            ImageUrl = createdService.ImageUrl,
            PublicId = createdService.PublicId,
            CreatedAt = createdService.CreatedAt,
            UpdatedAt = createdService.UpdatedAt
        };
    }

    public async Task<ServiceResponseDto> DeleteService(string id)
    {
        var service = await serviceRepository.DeleteService(id);

        if (service == null) throw new InvalidOperationException("Service not found");

        // Delete the image from Cloudinary using the public IDx
        await cloudinaryImageService.DeleteImageAsync(service.PublicId);

        return new ServiceResponseDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            ImageUrl = service.ImageUrl,
            PublicId = service.PublicId,
            CreatedAt = service.CreatedAt,
            UpdatedAt = service.UpdatedAt
        };
    }

    public async Task<ServiceResponseDto> UpdateService(ServiceUpdateRequestDto dto)
    {
        var imageUrl = dto.ImageUrl;
        var publicId = dto.PublicId;
        if (!string.IsNullOrEmpty(dto.ImageUrl))
        {
            // Convert Base64 string to byte array
            var imageBytes = Convert.FromBase64String(dto.ImageUrl);
            // Create a memory stream from the image bytes
            using var stream = new MemoryStream(imageBytes);
            // Generate a unique filename for the image
            // Upload the image stream to Cloudinary and get the image URL
            var uploadResult = await cloudinaryImageService.UploadImageAsync(stream, dto.PublicId);
            imageUrl = uploadResult.SecureUrl;
            publicId = uploadResult.PublicId;
        }

        var service = new Service
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = imageUrl,
            PublicId = publicId,
            UpdatedAt = DateTime.Now
        };

        var updatedService = await serviceRepository.UpdateService(service);
        return new ServiceResponseDto
        {
            Id = updatedService.Id,
            Name = updatedService.Name,
            Description = updatedService.Description,
            ImageUrl = updatedService.ImageUrl,
            PublicId = updatedService.PublicId,
            CreatedAt = updatedService.CreatedAt,
            UpdatedAt = updatedService.UpdatedAt
        };
    }

    public async Task<List<ServiceResponseDto>> GetAllServices()
    {
        var services = await serviceRepository.GetAllServices();

        return services.Select(service => new ServiceResponseDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            ImageUrl = service.ImageUrl,
            PublicId = service.PublicId,
            CreatedAt = service.CreatedAt,
            UpdatedAt = service.UpdatedAt
        }).ToList();
    }

    public async Task<ServiceResponseDto> GetServiceById(string id)
    {
        var services = await serviceRepository.GetServiceById(id);

        return new ServiceResponseDto
        {
            Id = services.Id,
            Name = services.Name,
            Description = services.Description,
            ImageUrl = services.ImageUrl,
            PublicId = services.PublicId,
            CreatedAt = services.CreatedAt,
            UpdatedAt = services.UpdatedAt
        };
    }
}