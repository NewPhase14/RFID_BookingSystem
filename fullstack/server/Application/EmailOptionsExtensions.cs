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
        {
            services
                .AddFluentEmail(emailOptions.SenderEmail, emailOptions.SenderName)
                .AddSmtpSender(new System.Net.Mail.SmtpClient
                {
                    Host = emailOptions.Host,
                    Port = emailOptions.Port,
                });
        }
        else
        {
            services
                .AddFluentEmail(emailOptions.SenderEmail, emailOptions.SenderName)
                .AddSmtpSender(new System.Net.Mail.SmtpClient
                {
                    Host = emailOptions.Host,
                    Port = emailOptions.Port,
                    Credentials = new System.Net.NetworkCredential(emailOptions.Username, emailOptions.Password),
                    EnableSsl = true
                });
        }
        return services;
    }
    

}