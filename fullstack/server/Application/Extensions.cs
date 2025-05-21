using Application.Interfaces;
using Application.Interfaces.Infrastructure.Websocket;
using Application.Services;
using Application.Validators.Auth;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace Application;

public static class Extensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {    
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAvailabilityService, AvailabilityService>();
        services.AddScoped<IActivityLogService, ActivityLogService>();
        services.AddScoped<ICheckingService, CheckingService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICloudinaryImageService, CloudinaryImageService>();
        services.AddValidatorsFromAssemblyContaining<AccountActivationValidator>();
        services.AddValidatorsFromAssemblyContaining<ForgotPasswordValidator>();
        services.AddValidatorsFromAssemblyContaining<ResetPasswordValidator>();
        return services;
    }
}