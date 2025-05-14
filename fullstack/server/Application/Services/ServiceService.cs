using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.Service;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Domain.Entities;

namespace Application.Services;

public class ServiceService(IServiceRepository serviceRepository, ICloudinaryImageService cloudinaryImageService) : IServiceService
{
    public async Task<ServiceResponseDto> CreateService(ServiceCreateRequestDto dto)
    {
        string imageUrl = string.Empty;
        
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
            imageUrl = await cloudinaryImageService.UploadImageAsync(stream, fileName);
        }
        
        var service = new Service()
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = imageUrl
        };
        serviceRepository.AddService(service);
        return new ServiceResponseDto()
        {
            Message = "Service created successfully"
        };
    }

    public ServiceResponseDto DeleteService(string id)
    {
        serviceRepository.DeleteService(id);
        return new ServiceResponseDto()
        {
            Message = "Service deleted successfully"
        };
    }

    public ServiceResponseDto UpdateService(ServiceUpdateRequestDto dto)
    {
        var service = new Service()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl
        };
        serviceRepository.UpdateService(service);
        return new ServiceResponseDto()
        {
            Message = "Service updated successfully"
        };
    }

    public List<GetAllServiceResponseDto> GetAllServices()
    {
        var services = serviceRepository.GetAllServices();
        
        return services.Select(service => new GetAllServiceResponseDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            ImageUrl = service.ImageUrl
        }).ToList();
        
    }
}