using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;

namespace Infrastructure.Postgres.Postgresql.Data;

public class AuthRepo(MyDbContext ctx) : IAuthDataRepository
{
    public User? GetUserOrNull(string email)
    {
        return ctx.Users.FirstOrDefault(u => u.Email == email);
    }

    public User AddUser(User user)
    {
        ctx.Users.Add(user);
        ctx.SaveChanges();
        return user;
    }

    public Role GetRole(string roleName)
    {
        return ctx.Roles.FirstOrDefault(r => r.Name == roleName) ?? throw new InvalidOperationException();
    }
}