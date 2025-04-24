using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Checking;

public class CheckBookingRequestDto
{
    [Required] public string Rfid { get; set; } = null!;
    [Required] public string ServiceId { get; set; } = null!;
}