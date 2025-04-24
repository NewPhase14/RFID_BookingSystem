using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Checking;

public class CheckingResponseDto
{
    [Required] public string Message { get; set; } = null!;
}