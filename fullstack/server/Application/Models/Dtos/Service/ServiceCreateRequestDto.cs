using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Service;

public class ServiceCreateRequestDto
{
    [Required] public string Name { get; set; } = null!;
}