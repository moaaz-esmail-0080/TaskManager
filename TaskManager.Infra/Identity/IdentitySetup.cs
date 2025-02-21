
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Entities;
using TaskManager.Infra.Context;

namespace TaskManager.Infra.Identity;

public static class IdentitySetup
{
    public static IServiceCollection AddIdentitySetup(this IServiceCollection services)
    {
        services.AddIdentity<TaskUser, TaskRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
