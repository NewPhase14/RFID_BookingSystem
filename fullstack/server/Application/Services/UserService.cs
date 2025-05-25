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
            CreatedAt = user.CreatedAt.ToString("dd-MM-yyyy HH:mm"),
            UpdatedAt = user.UpdatedAt.ToString("dd-MM-yyyy HH:mm")
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
            CreatedAt = user.CreatedAt.ToString("dd-MM-yyyy HH:mm"),
            UpdatedAt = user.UpdatedAt.ToString("dd-MM-yyyy HH:mm")
        }).ToList();
    }

    public UserResponseDto DeleteUser(string id)
    {
    var user = userRepository.DeleteUser(id);

        return new UserResponseDto
        {
            Id = user.Id
        };
    }
}