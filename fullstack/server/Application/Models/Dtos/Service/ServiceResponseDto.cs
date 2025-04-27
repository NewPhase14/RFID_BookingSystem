using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Service;

public class ServiceResponseDto
{ 
    [Required] public string Message { get; set; } = null!;
}