using Application.Models.Dtos.User;

namespace Application.Interfaces.Infrastructure.Websocket;

public interface IUserService
{
    public UserResponseDto GetUserByEmail(string email);
    public List<UserResponseDto> GetAllUsers();
    public UserResponseDto DeleteUser(string id);
}