using Serilog;
using TaskManager.Core.JWT;
using TaskManager.Core.Interfaces;
using Microsoft.Extensions.Options;
using TaskManager.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Interfaces.Repositories;
using TaskManager.Application.Commands;

using TaskManager.Infra.Identity.Services;
using Microsoft.Extensions.Configuration;
using TaskManager.Infra.Identity;
namespace TaskManager.Infra.IoC.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentitySetup();
        services.AddRepositories();
        services.AddServices();
        var jwtSettingsSection = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSettingsSection);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);
        services.AddSingleton(Log.Logger);
        services.AddLogging();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateTaskCommand).Assembly);
        });
        return services;
    }
}
