using System.Net;
using System.Net.Mail;
using Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class EmailOptionsExtensions
{
    public static IServiceCollection AddEmailOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));

        var emailOptions = configuration.GetSection("EmailOptions").Get<EmailOptions>();

        if (emailOptions.Password == null || emailOptions.Username == null)
            services
                .AddFluentEmail(emailOptions.SenderEmail, emailOptions.SenderName)
                .AddSmtpSender(new SmtpClient
                {
                    Host = emailOptions.Host,
                    Port = emailOptions.Port
                });
        else
            services
                .AddFluentEmail(emailOptions.SenderEmail, emailOptions.SenderName)
                .AddSmtpSender(new SmtpClient
                {
                    Host = emailOptions.Host,
                    Port = emailOptions.Port,
                    Credentials = new NetworkCredential(emailOptions.Username, emailOptions.Password),
                    EnableSsl = true
                });
        return services;
    }
}