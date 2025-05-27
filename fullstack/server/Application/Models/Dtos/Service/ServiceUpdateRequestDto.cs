using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Service;

public class ServiceUpdateRequestDto
{
    [Required] public string Id { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;
    [Required] public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    [Required] public string PublicId { get; set; } = null!;
    
}