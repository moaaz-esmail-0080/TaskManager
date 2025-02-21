using TaskManager.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Infra.IoC.Configurations;

public static class AutoMapperSetup
{
    public static IServiceCollection AddAutoMapperSetup(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        return services;
    }
}
