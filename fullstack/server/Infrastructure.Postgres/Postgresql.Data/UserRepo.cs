using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class UserRepo(MyDbContext context) : IUserRepository
{
    public User GetUserByEmail(string email)
    {
        var user = context.Users.
            Include(user => user.Role)
            .FirstOrDefault(u => u.Email == email);
        
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }
        
        return user;
    }

    public List<User> GetAllUsers()
    {
        var users = context.Users.
            Include(user => user.Role).
            Where(user => user.Role.Name == "User").ToList();
        if (users == null || users.Count == 0)
        {
            throw new InvalidOperationException("No users found");
        }
        
        return users;
    }

    public User DeleteUser(string id)
    {
        var user = context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }
        
        context.Users.Remove(user);
        context.SaveChanges();
        
        return user;
    }
}