using Application.Interfaces.Infrastructure.Postgres;
using Core.Domain.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Postgresql.Data;

public class UserRepo(MyDbContext context) : IUserRepository
{
    public async Task<User> GetUserByEmail(string email)
    {
        var user = await context.Users.Include(user => user.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null) throw new InvalidOperationException("User not found");

        return user;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await context.Users.Include(user => user.Role).Where(user => user.Role.Name == "User")
            .ToListAsync();
        if (users == null || users.Count == 0) throw new InvalidOperationException("No users found");

        return users;
    }

    public async Task<User> DeleteUser(string id)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) throw new InvalidOperationException("User not found");

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        var europeanTime = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeanTime);
        
        var existingUser = await context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == user.Id);
        if (existingUser == null) throw new InvalidOperationException("User not found");
        existingUser.UpdatedAt = now;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Rfid = user.Rfid;
        var updatedUser = context.Users.Update(existingUser);
        await context.SaveChangesAsync();
        return updatedUser.Entity;
    }
}