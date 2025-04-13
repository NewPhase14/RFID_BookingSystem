using Core.Domain.Entities;

namespace Application.Interfaces.Infrastructure.Postgres;

public interface IAuthDataRepository
{
    User? GetUserOrNull(string email);
    User AddUser(User user);
    Role GetRole(string roleName);
}