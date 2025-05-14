using Application.Interfaces.Infrastructure.Postgres;
using Application.Interfaces.Infrastructure.Websocket;
using Application.Models.Dtos.User;

namespace Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public UserResponseDto GetUserByEmail(string email)
    {
        var user = userRepository.GetUserByEmail(email);

        return new UserResponseDto
        {
            Id = user.Id,
            Rfid = user.Rfid,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RoleName = user.Role.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public List<UserResponseDto> GetAllUsers()
    {
        var users = userRepository.GetAllUsers();

        return users.Select(user => new UserResponseDto
        {
            Id = user.Id,
            Rfid = user.Rfid,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RoleName = user.Role.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        }).ToList();
    }
}