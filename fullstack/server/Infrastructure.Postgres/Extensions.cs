﻿using Application.Interfaces.Infrastructure.Postgres;
using Application.Models;
using Infrastructure.Postgres.Postgresql.Data;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Postgres;

public static class Extensions
{
    public static IServiceCollection AddDataSourceAndRepositories(this IServiceCollection services)
    {
        services.AddDbContext<MyDbContext>((service, options) =>
        {
            var provider = services.BuildServiceProvider();
            options.UseNpgsql(
                provider.GetRequiredService<IOptionsMonitor<AppOptions>>().CurrentValue.DbConnectionString);
            options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IAuthRepository, AuthRepo>();
        services.AddScoped<IBookingRepository, BookingRepo>();
        services.AddScoped<IActivityLogsRepository, ActivityLogsRepo>();
        services.AddScoped<IAvailabilityRepository, AvailabilityRepo>();
        services.AddScoped<ICheckingRepository, CheckingRepo>();
        services.AddScoped<IServiceRepository, ServiceRepo>();
        services.AddScoped<IUserRepository, UserRepo>();
        services.AddScoped<Seeder>();

        return services;
    }
}