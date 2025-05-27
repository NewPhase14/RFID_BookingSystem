using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Service;

public class ServiceCreateRequestDto
{
    [Required] public string Name { get; set; } = null!;
    [Required] public string Description { get; set; } = null!;
    [Required] public string ImageUrl { get; set; } = null!;
}