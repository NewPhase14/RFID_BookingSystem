using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.TimeSlot;

public class TimeSlotResponseDto
{
    [Required] public string Message { get; set; } = null!;
}