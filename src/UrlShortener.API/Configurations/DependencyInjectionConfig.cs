using MongoDB.Driver;
using UrlShortener.Infra.Configurations;

namespace UrlShortener.API.Configurations;
public static class DependencyInjectionConfig
{
    public static void AddApplicationServicesConfig(this IServiceCollection services)
    {

    }

    public static void AddIfrastructureServicesConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(
        configuration.GetSection("ApplicationDatabase"));
    }
}
