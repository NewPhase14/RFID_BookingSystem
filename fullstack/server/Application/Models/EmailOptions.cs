namespace Application.Models;

public class EmailOptions
{
    public string SenderEmail { get; set; } = null!;
    public string SenderName { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}