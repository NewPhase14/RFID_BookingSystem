using Application.Models.Dtos.User;

namespace Application.Interfaces.Infrastructure.Websocket;

public interface IUserService
{
    Task<UserResponseDto> GetUserByEmail(string email);
    Task<List<UserResponseDto>> GetAllUsers();
    Task<UserResponseDto> DeleteUser(string id);
}