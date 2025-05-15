using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Service;

public class GetServiceResponseDto
{
    [Required] public string Id { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;
    [Required] public string Description { get; set; } = null!;
    [Required] public string ImageUrl { get; set; } = null!;
    [Required] public string PublicId { get; set; } = null!;
    [Required] public DateTime CreatedAt { get; set; }
    [Required] public DateTime UpdatedAt { get; set; }
    
}