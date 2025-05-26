using Application.Interfaces;
using Application.Interfaces.Infrastructure.Postgres;
using Application.Models.Dtos.User;
using Core.Domain.Entities;

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

    public async Task<UserResponseDto> UpdateUser(UserUpdateRequestDto dto)
    {
        var user = new User()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Rfid = dto.Rfid
        };
        var updatedUser = await userRepository.UpdateUser(user);
        return new UserResponseDto()
        {
            Id = updatedUser.Id,
            Email = updatedUser.Email,
            FirstName = updatedUser.FirstName,
            LastName = updatedUser.LastName,
            Rfid = updatedUser.Rfid,
            RoleName = updatedUser.Role.Name,
            CreatedAt = updatedUser.CreatedAt.ToString("dd-MM-yyyy HH:mm"),
            UpdatedAt = updatedUser.UpdatedAt.ToString("dd-MM-yyyy HH:mm"),
        };
    }
}