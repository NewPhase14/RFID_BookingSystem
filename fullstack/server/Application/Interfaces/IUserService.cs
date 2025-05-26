using Application.Models.Dtos.User;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> GetUserByEmail(string email);
    Task<List<UserResponseDto>> GetAllUsers();
    Task<UserResponseDto> DeleteUser(string id);
    Task<UserResponseDto> UpdateUser(UserUpdateRequestDto dto);
}