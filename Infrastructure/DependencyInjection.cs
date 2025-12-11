using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? serverConnectionString = configuration.GetConnectionString("ScriptoriumDatabase");
        services.AddDbContext<ScriptoriumDbContext>(options => 
            options.UseMySql(serverConnectionString, ServerVersion.AutoDetect(serverConnectionString)));
        return services;
    }
}