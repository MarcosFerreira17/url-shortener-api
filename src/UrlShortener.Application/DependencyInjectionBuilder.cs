using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Services;
using UrlShortener.Domain.Common.Interfaces.Repositories;
using UrlShortener.Domain.Url.Repositories.Interfaces;
using UrlShortener.Infra.Configurations;
using UrlShortener.Infra.Repositories;

namespace UrlShortener.Application;
public static class DependencyInjectionBuilder
{
    public static void InjectApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUrlService, UrlService>();
    }

    public static void InjectInfraDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));
        services.AddSingleton<IUrlRepository, UrlRepository>();
    }
}
