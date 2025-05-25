using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IUserRepository
{
    User GetUserByEmail(string email);
    List<User> GetAllUsers();
    User DeleteUser(string id);
}