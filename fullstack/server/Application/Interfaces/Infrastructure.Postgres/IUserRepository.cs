using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
    Task<List<User>> GetAllUsers();
    Task<User> DeleteUser(string id);
    Task<User> UpdateUser(User user);
}