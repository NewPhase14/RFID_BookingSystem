using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos;

public class AuthRegisterRequestDto
{
    [Required] public string Rfid { get; set; } = null!;
    [Required] public string Email { get; set; } = null!;
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;

}