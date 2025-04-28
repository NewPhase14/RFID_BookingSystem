using Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class EmailOptionsExtensions
{
    public static IServiceCollection AddEmailOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
        services
            .AddFluentEmail(configuration["EmailOptions:SenderEmail"], configuration["EmailOptions:SenderName"])
            .AddSmtpSender(configuration["EmailOptions:Host"], configuration.GetValue<int>("EmailOptions:Port"));
        
        return services;
    }
    

}