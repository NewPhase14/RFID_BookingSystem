using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Availability;

public class AvailabilityResponseDto
{
    [Required] public string Message { get; set; } = null!;
}