using Application.Models;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Application;

public static class CloudinaryOptionsExtensions
{
    public static IServiceCollection AddCloudinaryOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CloudinaryOptions>(configuration.GetSection("CloudinaryOptions"));
        
        configuration.GetSection("CloudinaryOptions").Get<CloudinaryOptions>();
        
        services.AddSingleton(provider =>
        {
            var options = provider.GetRequiredService<IOptions<CloudinaryOptions>>().Value;
            var account = new Account(options.CloudName, options.ApiKey, options.ApiSecret);
            return new Cloudinary(account);
        });
        
        return services;

    }
}