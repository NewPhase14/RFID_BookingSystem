using Application.Interfaces.Infrastructure.Postgres;
using Application.Interfaces.Infrastructure.Websocket;
using Application.Models.Dtos.User;

namespace Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserResponseDto> GetUserByEmail(string email)
    {
        var user = await userRepository.GetUserByEmail(email);

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

    public async Task<List<UserResponseDto>> GetAllUsers()
    {
        var users = await userRepository.GetAllUsers();

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

    public async Task<UserResponseDto> DeleteUser(string id)
    {
    var user = await userRepository.DeleteUser(id);

        return new UserResponseDto
        {
            Id = user.Id
        };
    }
}