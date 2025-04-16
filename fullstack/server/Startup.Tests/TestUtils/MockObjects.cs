using Core.Domain.Entities;

namespace Startup.Tests.TestUtils;

public static class MockObjects
{
    public static User GetUser(string? role = null)
    {
        return new User
        {
            
        };
    }
}