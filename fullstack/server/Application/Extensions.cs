using Application.Interfaces;
using Application.Models;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Extensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {    
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAvailabilityService, AvailabilityService>();
        services.AddScoped<ICheckingService, CheckingService>();
        services.AddScoped<IServiceService, ServiceService>();
        return services;
    }
}